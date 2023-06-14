namespace CheckpointMobile1.Singletons
{
    public class CheckpointAccessedList
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleOffice { get; set; }
        public bool IsActive { get; set; }
    }
    public class CheckpointSingleton
    {
        private static CheckpointSingleton _instance;
        private CheckpointAccessedList _checkpoint;

        private CheckpointSingleton()
        {
            // Приватный конструктор для предотвращения создания экземпляров извне
        }

        public static CheckpointSingleton Instance
        {
            get
            {
                // Ленивая инициализация экземпляра синглтона
                if (_instance == null)
                {
                    _instance = new CheckpointSingleton();
                }
                return _instance;
            }
        }

        public CheckpointAccessedList Checkpoint
        {
            get { return _checkpoint; }
            set { _checkpoint = value; }
        }
    }
}
