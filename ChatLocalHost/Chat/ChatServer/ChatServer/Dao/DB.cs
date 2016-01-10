using Chat;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ChatServer.Service;

namespace ChatServer.Dao
{
    public class DB
    {

        public static void Commit()
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "commit";

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateStatusOfflineAll()
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE users SET status = 0 WHERE id IN(SELECT id FROM users WHERE status = 1)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IClient User(string user, string pass)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT id,username,status FROM users WHERE username = @user AND pass = @pass";

                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (!reader.Read())
                        throw new InvalidUserException("Wrongh user or pass!");

                    if (reader.GetBoolean(2) == true)
                        throw new InvalidUserException("You are already logged in. Please log off first.");
                    IClient client = Service.Server.User(reader.GetInt32(0));

                    if (client != null)
                        return client;

                    return new Client(reader.GetInt32(0),
                        reader.GetString(1), reader.GetBoolean(2));

                }

            }
        }

        public bool Register(string user, string pass)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = c;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * FROM users WHERE username = @user";
                    cmd.Parameters.AddWithValue("@user", user);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        return false;

                    reader.Close();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO users VALUES(@user,@pass,0)";
                    cmd.Parameters.AddWithValue("@pass", pass);
                    return cmd.ExecuteNonQuery() == 1 ? true : false;
                }
            }
        }

        public void UpdateStatus(int id, bool status)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE users SET status = @status WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@status", status ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int FriendRequestCount(int id)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT count(*) FROM friends_pending WHERE toid = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<IClient> FriendRequest(int id)
        {
            List<IClient> arr = new List<IClient>();
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT id,username,status FROM users WHERE id in (SELECT fromid FROM friends_pending WHERE toid = @id)";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Client client = new Client(reader.GetInt32(0),
                            reader.GetString(1), reader.GetBoolean(2));
                        arr.Add(client);
                    }
                    return arr;
                }
            }
        }

        public List<IClient> SearchUser(string user, int id)
        {
            List<IClient> arr = new List<IClient>();
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("SELECT id,username,status FROM users WHERE id != {0} AND username LIKE '%{1}%' AND (id NOT IN ({2}) OR id NOT IN ({3}))",
                        id.ToString(),
                        user,
                        "SELECT user1 FROM friends WHERE user2 = " + id,
                        "SELECT user2 FROM friends WHERE user1 = " + id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        IClient client = Service.Server.User(reader.GetInt32(0));

                        if (client == null)
                            client = new Client(reader.GetInt32(0), reader.GetString(1),
                                reader.GetBoolean(2));
                        arr.Add(client);
                    }
                    return arr;
                }
            }
        }

        public void AddFriend(int from, int to, bool accept)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM friends_pending WHERE fromid = @from AND toid = @to";

                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    cmd.ExecuteNonQuery();
                    if (accept)
                    {
                        cmd.CommandText = "INSERT INTO friends values(@from,@to)";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<IClient> GetFriends(int id)
        {
            List<IClient> arr = new List<IClient>();
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT id,username,status FROM users WHERE id IN (SELECT user1 FROM friends WHERE user2 = @id) OR id IN(SELECT user2 FROM friends WHERE user1 = @id)";


                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        IClient client = Service.Server.User(reader.GetInt32(0));

                        if (client == null)
                            client = new Client(reader.GetInt32(0), reader.GetString(1),
                                reader.GetBoolean(2));
                        arr.Add(client);
                    }
                    return arr;
                }
            }
        }

        public List<IClient> GetAllUsers()
        {
            List<IClient> arr = new List<IClient>();
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT id,username,status FROM users";

                    //cmd.Parameters.AddWithValue("@id", currentId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        IClient client = Service.Server.User(reader.GetInt32(0));

                        if (client == null)
                            client = new Client(reader.GetInt32(0), reader.GetString(1),
                                reader.GetBoolean(2));
                        arr.Add(client);
                    }
                    return arr;
                }
            }
        }

        public void FriendRequest(int from, int to)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO friends_pending VALUES(@from,@to)";


                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int AddMessage(int from, int to, string msg)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO history OUTPUT Inserted.id VALUES(@from,@to,GETDATE(),@msg,0)";


                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    cmd.Parameters.AddWithValue("@msg", msg);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public bool GotMessage(int from, int to)
        {
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT timestamp,message FROM history WHERE fromid = @from AND toid = @to AND status = 0";


                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.HasRows;
                }

            }
        }
        public List<MessageContainer> GetMessage(IClient from, IClient to, bool b)
        {
            List<MessageContainer> arr = new List<MessageContainer>();
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT timestamp,message FROM history WHERE fromid = @from AND toid = @to AND status = 0";


                    cmd.Parameters.AddWithValue("@from", from.ID);
                    cmd.Parameters.AddWithValue("@to", to.ID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MessageContainer msc = new MessageContainer();
                        msc.From = from;
                        msc.To = to;
                        msc.TimeStamp = reader.GetDateTime(0);
                        msc.Message = reader.GetString(1);
                        arr.Add(msc);
                    }
                    if (b)
                    {
                        reader.Close();

                        cmd.CommandText = "UPDATE history SET status = 1 WHERE fromid = @from AND toid = @to AND status = 0";
                        cmd.ExecuteNonQuery();
                    }
                    return arr;
                }
            }
        }

        public List<MessageContainer> GetHistory(IClient from, IClient to, int rows)
        {
            List<MessageContainer> arr = new List<MessageContainer>();
            using (SqlConnection c = new SqlConnection(Properties.Settings.Default.chatConnectionString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = c;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("SELECT TOP {0} fromid,toid,timestamp,message FROM history WHERE(fromid = @from AND toid = @to) OR(fromid = @to AND toid = @from) ORDER BY id DESC",rows);


                    cmd.Parameters.AddWithValue("@from", from.ID);
                    cmd.Parameters.AddWithValue("@to", to.ID);                  

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MessageContainer msc = new MessageContainer();
                        msc.From = reader.GetInt32(0) == from.ID ? from : to;
                        msc.To = reader.GetInt32(1) == from.ID ? from : to;
                        msc.TimeStamp = reader.GetDateTime(2);
                        msc.Message = reader.GetString(3);
                        arr.Add(msc);
                    }
                    return arr;
                }
            }
        }
    }
}
