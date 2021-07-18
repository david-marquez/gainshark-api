using gainshark_api.Encryption.Contract;
using gainshark_api.Mappers.Implementation;
using gainshark_api.Models;
using gainshark_api.MySqlProvider.Contract;
using gainshark_api.Repositories.Contract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace gainshark_api.Repositories.Implementation
{
	public class UserRepository : IRepository<User>
	{
		private IMySqlProvider<User> _userMySqlProvider;
		private IMySqlProvider<Program> _programMySqlProvider;
		private IBCryptEncryption _bCryptEncryption;

		public UserRepository(IMySqlProvider<User> userMySqlProvider,
			IMySqlProvider<Program> programMySqlProvider,
			IBCryptEncryption bCryptEncryption)
		{
			_userMySqlProvider = userMySqlProvider;
			_programMySqlProvider = programMySqlProvider;
			_bCryptEncryption = bCryptEncryption;
		}

		public void AddItem(User user)
		{
			string sql = @"	INSERT	INTO tbl_users
							(	User_FirstName,
								User_LastName,
								User_UserName,
								User_Email,
								User_Password,
								User_Role
							)
							VALUES(
								@User_FirstName,
								@User_LastName,
								@User_UserName,
								@User_Email,
								@User_Password,
								@User_RoleId
							);";

			var engine = _userMySqlProvider.GetEngine();

			engine.AddItem(sql, UserParameters(user));
		}

		public void DeleteItem(int id)
		{
			string sql = @"	/* Delete programs */

							DELETE	FROM tbl_programs
							WHERE	User_Id = @Id;

							/* Delete user */

							DELETE	FROM tbl_users
							WHERE	User_Id = @Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Id", id)
			};

			var engine = _userMySqlProvider.GetEngine();

			engine.DeleteItem(sql, mySqlParameters);
		}

		public User GetItem(int id)
		{
			string userSql = @"	SELECT	user.User_Id,
										user.User_FirstName,
										user.User_LastName,
										user.User_UserName,
										user.User_Email,
										null as Password,
										role.Role_Id,
										role.Role_Name,
										role.Role_Description

								FROM	tbl_users user, tbl_roles role

								WHERE	role.Role_Id = user.User_Role
								AND		user.User_Id = @User_Id;";

			string programSql = @"	SELECT	program.Program_Id,
											program.User_Id,
											program.Program_Name,
											program.Program_Description

									FROM	tbl_programs program

									WHERE	program.User_Id = @User_Id;";

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@User_Id", id)
			};

			var userEngine = _userMySqlProvider.GetEngine();
			var programEngine = _programMySqlProvider.GetEngine();

			User user = userEngine.GetItem(userSql, 
											mySqlParameters, 
											new UserMapper());

			user.Programs = (List<Program>)programEngine.GetItems(programSql,
																	mySqlParameters,
																	new ProgramMapper());

			return user;
		}

		public IList<User> GetItems()
		{
			string userSql = @"	SELECT	user.User_Id,
										user.User_FirstName,
										user.User_LastName,
										user.User_UserName,
										user.User_Email,
										null as Password,
										role.Role_Id,
										role.Role_Name,
										role.Role_Description

								FROM	tbl_users user, tbl_roles role

								WHERE	role.Role_Id = user.User_Role;";

			string programSql = @"	SELECT	program.Program_Id,
											program.User_Id,
											program.Program_Name,
											program.Program_Description

									FROM	tbl_programs program

									WHERE	program.User_Id = @User_Id;";

			var userEngine = _userMySqlProvider.GetEngine();
			var programEngine = _programMySqlProvider.GetEngine();

			List<User> users = (List<User>)userEngine.GetItems(userSql,
																null,
																new UserMapper());

			foreach(User user in users)
			{
				List<MySqlParameter> programParameters = new List<MySqlParameter>()
				{
					new MySqlParameter("@User_Id", user.Id)
				};

				user.Programs = (List<Program>)programEngine.GetItems(programSql,
																		programParameters,
																		new ProgramMapper());
			}

			return users;
		}

		public void UpdateItem(User user)
		{
			string sql = @"	INSERT	INTO tbl_users
							(	User_FirstName,
								User_LastName,
								User_UserName,
								User_Email,
								User_Password,
								User_Role
							)
							VALUES(
								@User_FirstName,
								@User_LastName,
								@User_UserName,
								@User_Email,
								@User_Password,
								@User_RoleId
							)
							ON	DUPLICATE KEY UPDATE
								User_FirstName = @User_FirstName,
								User_LastName = @User_LastName,
								User_Email = @User_Email,
								User_Role = @User_RoleId,
								User_Password = @User_Password";

			var engine = _userMySqlProvider.GetEngine();

			engine.AddItem(sql, UserParameters(user));
		}

		private IList<MySqlParameter> UserParameters(User user)
		{
			string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(user.Password));

			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@User_Id", user.Id),
				new MySqlParameter("@User_FirstName", user.FirstName),
				new MySqlParameter("@User_LastName", user.LastName),
				new MySqlParameter("@User_UserName", user.UserName),
				new MySqlParameter("@User_Email", user.Email),
				new MySqlParameter("@User_RoleId", user.Role.Id),
				new MySqlParameter("@User_Password", _bCryptEncryption.Hash(decodedPassword))
			};

			return mySqlParameters;
		}

		private IList<MySqlParameter> ProgramParameters(Program program)
		{
			List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@Program_Id", program.Id),
				new MySqlParameter("@Program_UserId", program.UserId),
				new MySqlParameter("@Program_Name", program.Name),
				new MySqlParameter("@Program_Description", program.Description)
			};

			return mySqlParameters;
		}
	}
}