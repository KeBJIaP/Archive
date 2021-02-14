namespace GZipTest.Components.UserInteraction
{
    /// <summary>
    /// Взаимодействие с пользователем
    /// </summary>
    public interface IUserInteractionsService
    {
        /// <summary>
        /// Спросить у пользователя с вариантами ответа да/нет
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <returns>true если пользователь ответил да</returns>
        bool AskYesNo(string message);
    }
}
