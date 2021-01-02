namespace Blogifier.Shared
{
	public enum GroupAction
   {
      Publish, Unpublish, Feature, Delete
   }

   public enum PublishedStatus
   {
      All, Published, Drafts, Featured
   }

   public enum PostListType
   {
      Blog, Category, Author, Search
   }

   public enum PostAction
   {
      Save, Publish, Unpublish
   }

   public enum PostType
   {
      Post = 1, Page = 2
   }

   public enum SaveStatus
   {
      Saving = 1, Publishing = 2, Unpublishing = 3
   }

   public enum UploadType
   {
      Avatar,
      Attachement,
      AppLogo,
      AppCover,
      PostCover,
      PostImage
   }

   public enum ThemeOptions
	{
      Text,
      TextArea,
      Checkbox,
      Select
	}

   public enum Status
   {
      Success, Warning, Error
   }

   public enum ImportType
   {
      Post, Image, Attachement
   }
}
