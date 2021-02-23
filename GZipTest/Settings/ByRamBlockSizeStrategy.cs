using Archive.Application.Common;
using System;
using System.Management;

namespace GZipTest.Settings
{
    internal class ByRamBlockSizeStrategy : IBlockSizeStrategy
    {
        /// <summary>
        /// Размер блока по умолчанию
        /// </summary>
        private const int DefaultBlockSize = 1 * 1000 * 1000;
        /// <summary>
        /// Размер блока при недостаточном ОЗУ
        /// </summary>
        private const int SmallBlockSize = 50 * 1000;

        /// <summary>
        /// Минимальное значение ОЗУ
        /// </summary>
        private const long RamTreshhold = 8 * 1000 * 1000 * 1000l;

        private readonly ILogger _logger;

        public ByRamBlockSizeStrategy(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public int Calculate()
        {
            var ramsize = GetRamsizeOrNull();

            //если не знаем сколько ОЗУ или его достаточно - берем блок по умолчанию
            if (ramsize == null || ramsize >= RamTreshhold)
            {
                return DefaultBlockSize;
            }
            //если ОЗУ мало - берем маленький блок
            else
            {
                return SmallBlockSize;
            }
        }

        private long? GetRamsizeOrNull()
        {
            try
            {
                ManagementObjectSearcher ramMonitor =    //запрос к WMI для получения памяти ПК
                  new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

                foreach (var objram in ramMonitor.Get()) //нет ни индексера ни коллекции
                {
                    return (long?)Convert.ToUInt64(objram["TotalVisibleMemorySize"]);    //общая память ОЗУ
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Warning("Не удалось определить размер ОЗУ", ex);
                return null;
            }
        }
    }
}
