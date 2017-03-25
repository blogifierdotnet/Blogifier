namespace Blogifier.Core.Common
{
    public class Pager
    {
        public Pager(int currentPage, int itemsPerPage = 0)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;

            if (ItemsPerPage == 0)
                ItemsPerPage = ApplicationSettings.ItemsPerPage;

            Newer = CurrentPage - 1;
            ShowNewer = CurrentPage > 1 ? true : false;

            Older = currentPage + 1;
        }

        public void Configure(int total)
        {
            Total = total;
            var lastItem = CurrentPage * ItemsPerPage;
            ShowOlder = total > lastItem ? true : false;
            if (CurrentPage < 1 || lastItem > total + ItemsPerPage)
            {
                NotFound = true;
            }
            LastPage = (total % ItemsPerPage) == 0 ? total / ItemsPerPage : (total / ItemsPerPage) + 1;
        }

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int Total { get; set; }
        public bool NotFound { get; set; }

        public int Newer { get; set; }
        public bool ShowNewer { get; set; }

        public int Older { get; set; }
        public bool ShowOlder { get; set; }

        public int LastPage { get; set; }
    }
}
