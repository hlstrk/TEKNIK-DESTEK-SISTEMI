using LinqKit;
using Microsoft.AspNet.SignalR;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace TeknikServis.MvcUI
{
    public class NotificationComponent
    {

        ITeknikService teknikService = new TeknikManager(new EfTeknikRepository());

        public void RegisterNotification(DateTime currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["TeknikServisContext"].ConnectionString;
            string sqlCommand = @"SELECT [AtanmisPersonel],[TeknikServisID] from [dbo].[Teknikler] where [SonIslemZamani] > @AddedOn";


            using (SqlConnection con = new SqlConnection(conStr))

            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@AddedOn", currentTime);

                if (con.State!=System.Data.ConnectionState.Open)
                {
                    con.Open();

                }

                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader=cmd.ExecuteReader())
                        {

                }
            }

        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {

            if (e.Type==SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;

                sqlDep.OnChange -= sqlDep_OnChange;

                //Buradan Client'a mesaj gönderilecek

                

               


                RegisterNotification(DateTime.Now);

            }

        }

        








    }
}