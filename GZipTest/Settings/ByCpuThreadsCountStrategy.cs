namespace GZipTest.Settings
{
    internal class ByCpuThreadsCountStrategy : IThreadsCountStrategy
    {
        public int Calculate()
        {
            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }
            //штуки вроде HyperThreading уже должны быть учтеныw
            return coreCount-1;
        }
    }
}
