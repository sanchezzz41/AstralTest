namespace AstralTest.Identity.JWTAuthorization
{
    /// <summary>
    /// Класс для настройки токена
    /// </summary>
    public class TokenOption
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public string Audince { get; set; }

        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Время жизни токена
        /// </summary>
        public int LifeTime { get; set; }
    }
}
