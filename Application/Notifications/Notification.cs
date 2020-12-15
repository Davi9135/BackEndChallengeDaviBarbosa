namespace Application.Notifications
{
    public class Notification
    {
        public string Key { get; }
        public string Message { get; }

        public Notification(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public static Notification GetBadRequestNotification()
        {
            return new Notification("BadRequest", "Bad Request");
        }

        public static Notification GetInternalServerErrorNotification()
        {
            return new Notification("InternalServerError", "Internal Server Error");
        }
    }
}