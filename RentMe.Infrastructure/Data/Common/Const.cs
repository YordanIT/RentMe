namespace RentMe.Infrastructure.Data.Common
{
    public class Const
    {
        //User
        public const int FirstNameMaxLength = 100;
        public const int LastNameMaxLength = 100;
                 
        //Expense
        public const int CommentMaxLength = 200;

        //Property
        public const int PropertyTypeMaxLength = 100;
        public const int CityMaxLength = 100;
        public const int AddressMaxLength = 200;
        public const double AreaMinLength = 5;
        public const double AreaMaxLength = 2000;

        //Image
        public const int ImageMaxValue = 1048576;
        public const int DescriptionMaxLength = 200;

        //Article
        public const int TitleMaxLength = 50;
        public const int ContentMaxLength = 4000;
    }
}
