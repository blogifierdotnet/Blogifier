namespace Blogifier.Shared
{
	public class Pager
	{
      public Pager(int currentPage, int itemsPerPage = 0)
      {
         CurrentPage = currentPage;
         ItemsPerPage = itemsPerPage;

         if (ItemsPerPage == 0)
            ItemsPerPage = 10;

         Newer = CurrentPage - 1;
         ShowNewer = CurrentPage > 1 ? true : false;

         Older = currentPage + 1;
      }

      public void Configure(int total)
      {
         if (total == 0)
            return;

         if (ItemsPerPage == 0)
            ItemsPerPage = 10;

         Total = total;
         var lastItem = CurrentPage * ItemsPerPage;
         ShowOlder = total > lastItem ? true : false;
         if (CurrentPage < 1 || lastItem > total + ItemsPerPage)
         {
            NotFound = true;
         }
         LastPage = (total % ItemsPerPage) == 0 ? total / ItemsPerPage : (total / ItemsPerPage) + 1;
         if (LastPage == 0) LastPage = 1;
      }

      public int CurrentPage { get; set; } = 1;
      public int ItemsPerPage { get; set; }
      public int Total { get; set; }
      public bool NotFound { get; set; }

      public int Newer { get; set; }
      public bool ShowNewer { get; set; }

      public int Older { get; set; }
      public bool ShowOlder { get; set; }

      public string LinkToNewer { get; set; }
      public string LinkToOlder { get; set; }

      public string RouteValue { get; set; }
      public int LastPage { get; set; } = 1;
   }
}
