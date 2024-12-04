using BlazorFluxorPractice.Models;

namespace BlazorFluxorPractice.States.Users
{
    #region MyRegion

    public record FetchUsersAction;
    public record LoadUsersAction { public List<User> Users { get; set; } }
    public record LoadingUsersAction;
    public record LoadUsersFailedAction;

    #endregion

    #region DeleteUsers Actions

    public record DeleteUserAction
    {
        public int userId { get; set; }
        
        public DeleteUserAction(int userId)
        {
            this.userId = userId;
        }
    };

    public record DeleteUserActionSuccess
    {
        public int userId { get; set; }
        
        public DeleteUserActionSuccess(int userId)
        {
            this.userId = userId;
        }
    }

    public record DeleteUserActionFailure;

    #endregion
    
    #region CreateUsers Actions

    public record CreateUserAction
    {
        public User User { get; set; }
        
        public CreateUserAction(User user)
        {
            User = user;
        }
    }
    
    public record CreateUserActionSuccess
    {
        public User User { get; init; }
        public CreateUserActionSuccess(User user)
        {
            User = user;
        }
    }

    public record CrateUserActionFailure {}
    
    #endregion

    #region UpdateUser Action

    public class UpdateUserAction
    {
        public User User { get; init; }
        
        public UpdateUserAction(User user)
        {
            User = user;
        }
    } 
    
    public class UpdateUserActionSuccess
    {
        public User User { get; init; }
        
        public UpdateUserActionSuccess(User user)
        {
            User = user;
        }
    } 
    
    public record UpdateUserActionFailure {}

    #endregion
};