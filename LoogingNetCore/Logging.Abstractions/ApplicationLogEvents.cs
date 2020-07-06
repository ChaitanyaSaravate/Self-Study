namespace Logging.Abstractions
{
    public static class ApplicationLogEvents
    {
        public enum AuditEventTypes
        {
            StudentInfoOpened,
            StudentCreated,
            StudentPlaced,
            UserLoggedIn,
            UserLoggedOut,
            UserRoleChangedTo,
            StudentsLookUp,
            SchoolsLookUp,
            SchoolAdded
        }
    }
}
