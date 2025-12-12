namespace WebApp.Models
{
    /// <summary>
    /// Глобальная статистика приложения (аналог Application).
    /// Один экземпляр на всё приложение (Singleton).
    /// </summary>
    public class AppStats
    {
        private long _homeViews;
        private long _catalogViews;

        private readonly object _lock = new();

        public void IncrementHome()
        {
            lock (_lock)
            {
                _homeViews++;
            }
        }

        public void IncrementCatalog()
        {
            lock (_lock)
            {
                _catalogViews++;
            }
        }

        public long HomeViews => _homeViews;
        public long CatalogViews => _catalogViews;
        public long TotalViews => _homeViews + _catalogViews;
    }
}
