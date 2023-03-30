namespace KanbanBlazorClient.Store
{
    public class FilterStore
    {
        private string _query;
        public string Query { get { return _query; } set { _query = value; NotifyStateChanged(); } }

        public event Action OnChnage;

        private void NotifyStateChanged() => OnChnage?.Invoke();
    }
}
