namespace IdeoPrivateRoom.WebApi.Configurations;

public class IdeoAppRole
{
    public const string VacationServiceAdministrator = "Ideo.VacationService.Admin";
    public const string VacationServiceUser = "Ideo.VacationService.User";
    public const string VacationServiceAdminOrUser = VacationServiceAdministrator + "," + VacationServiceUser;
}
