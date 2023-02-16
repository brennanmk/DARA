/*
Brennan Miller-Klugman

database_query_handler is used to handle all queries to the database. This approach was taken so that the database login information is stored in a central location

References:
*/
using MySqlConnector;

public class database_query_handler
{
    private MySqlConnection robot_conn, augment_conn;

    public database_query_handler()
    {
        string robot_string = @"server=98.229.202.174;port=3307;user=root;password=yG6DH8W>bhR#}.0Q;database=ar;";
        this.robot_conn = new MySqlConnection(robot_string);
        robot_conn.Open();


    }


    public MySqlDataReader query(string sql){
        MySqlCommand cmd = new MySqlCommand(sql, robot_conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        return rdr;
    }
}
