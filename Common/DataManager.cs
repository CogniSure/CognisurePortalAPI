using Models.DTO;

public class DataManager
{
    public static List<Notification> GetData()
    {
        var r = new Random();
        return new List<Notification>()
        {
            new Notification { AccountID = 1,AccountName="abc",NotificationID=1, NotificationName ="adsajdkajd", IsNotificationRead =true, NotificationCount = 100 }
            ,new Notification { AccountID = 1,AccountName="abcsd",NotificationID=1, NotificationName ="fdefs", IsNotificationRead =true, NotificationCount = 100 }
            ,new Notification { AccountID = 1,AccountName="abcdf",NotificationID=1, NotificationName ="adfdsfsfsajdkajd", IsNotificationRead =false, NotificationCount = 100 }
            ,new Notification { AccountID = 1,AccountName="abcsss",NotificationID=1, NotificationName ="afdsfsfdsajdkajd", IsNotificationRead =true, NotificationCount = 100 }
        };
    }
}