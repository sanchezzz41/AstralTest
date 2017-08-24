namespace AstralTest.Identity.JWTAuthorization
{
    /// <summary>
    /// Класс содержащий информацию о токене
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Предоставляет токен
        /// </summary>
        public string TokenContent { get; set; }
        /// <summary>
        /// Предоставляет имя пользователя, который владаеет токеном
        /// </summary>
        public string TokenType { get; set; }
    }
}
