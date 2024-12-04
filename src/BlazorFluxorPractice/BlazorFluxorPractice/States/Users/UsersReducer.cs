using System.Text.Json;
using Fluxor;

namespace BlazorFluxorPractice.States.Users
{
    public class UsersReducer
    {

        #region ListUsers Reducers

        [ReducerMethod]
        public static UserState OnLoadingUsers(UserState state, LoadingUsersAction action)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static UserState OnStoreUsers(UserState state, LoadUsersAction action)
        {
            var sortedUsers = action.Users.OrderByDescending(user => user.UserId).ToList();

            var newState = state with { Users = sortedUsers, IsLoading = false, IsFirstLoading = false };

            return newState;
        }

        [ReducerMethod]
        public static UserState OnLoadUsersFailed(UserState state, LoadUsersFailedAction action)
            => state with { IsLoading = false };

        #endregion

        #region DeleteUser Reducers

        [ReducerMethod]
        public static UserState DeleteUserActionSuccess(UserState state, DeleteUserActionSuccess action)
        {
            
            Console.WriteLine($"el userId a eliminar es {action.userId}");
            
            var usersFiltered = state.Users.Where(u => u.UserId != action.userId);

            return new UserState
            {
                IsLoading = false,
                Users = usersFiltered.ToList(),
                IsFirstLoading = false
            };
        }

        #endregion

        #region CreateUserReducer

        [ReducerMethod]
        public static UserState CreateUserActionSuccess(UserState state, CreateUserActionSuccess action)
        {
            var newUsers = state.Users.ToList();

            newUsers.Insert(0, action.User);

            return state with { Users = newUsers, IsLoading = false, IsFirstLoading = false };
        }

        #endregion
        
        #region UpdateUserReducer

        [ReducerMethod]
        public static UserState UpdateUserActionSuccess(UserState state, UpdateUserActionSuccess action)
        {
            var index = state.Users.FindIndex(u => u.UserId == action.User.UserId);

            if ( index >= 0 )
            {
                state.Users[index] = action.User;
            }

            return state;
        }

        #endregion
    }
};

