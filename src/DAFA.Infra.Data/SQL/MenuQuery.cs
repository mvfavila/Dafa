namespace DAFA.Infra.Data.SQL
{
    public static class MenuQuery
    {
        public static readonly string GET_ALL_MENU_ITEMS = @"
            SELECT *
            FROM MenuItem m";
    }
}
