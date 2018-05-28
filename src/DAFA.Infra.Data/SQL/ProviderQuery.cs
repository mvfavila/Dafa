namespace DAFA.Infra.Data.SQL
{
	public class ProviderQuery
	{
		public static readonly string GET_ALL = @"
			SELECT p.*,
				   e.EmployeeId as 'Id',
				   e.*
			FROM Provider p
			INNER JOIN Employee e ON p.ProviderId = e.ProviderId";

		public static readonly string GET_BY_ID = @"
			SELECT *
			FROM Provider p
			WHERE p.ID = @ID";

		public static readonly string GET_WITH_EMPLOYEE_BY_ID = @"
			SELECT p.*,
				   e.EmployeeId as 'Id',
				   e.*
			FROM Provider p
			INNER JOIN Employee e ON p.ProviderId = e.ProviderId
			WHERE e.UserId = @ID";
	}
}
