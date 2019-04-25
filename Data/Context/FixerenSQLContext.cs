﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class FixerenSQLContext : IFixerenContext
    {
        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();

        public FixerenSQLContext()
        {
            connectie = dbconn.GetConnString();
        }

        public void TaakFixerenMetDocentID(int docentID, int taakID)
        {
            try
            {
                connectie.Open();
                

                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@docent", docentID);
                cmd.Parameters.AddWithValue("@taak", taakID);
                cmd.CommandText = "INSERT INTO [dbo].[GefixeerdeTaken] (DocentID, Taak_ID) VALUES (@docent, @taak)";
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connectie.Close();
            }
        }

        public void VerwijderGefixeerdeTaak(int fixID)
        {
            try
            {
                connectie.Open();
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "DELETE FROM [dbo].[GefixeerdeTaken] WHERE Fix_id ="+fixID+")";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // TODO: Fill Catch
            }
            finally
            {
                connectie.Close();
            }
        }

        public void VeranderGefixeerdeTaak(int taakID, int docentID)
        {
            try
            {
                connectie.Open();
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "UPDATE [dbo].[GefixeerdeTaken] SET Taak_id = " + taakID + ", DocentID =" + docentID + ")";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // TODO: Fill Catch
            }
            finally
            {
                connectie.Close();
            }
        }

        public List<GefixeerdeTaak> HaalAlleGefixeerdeTakenOp()
        {
            List<GefixeerdeTaak> GefixeerdeTaken = null;
            try
            {
                connectie.Open();
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "SELECT F.*, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[GefixeerdeTaken] F INNER JOIN [dbo].[Docent] D ON F.DocentID = D.DocentID INNER JOIN [dbo].[AspNetUsers] ANU ON D.MedewerkerID = ANU.Id";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GefixeerdeTaak taak = new GefixeerdeTaak();
                    taak.Fix_id = (int)reader["Fix_id"];
                    taak.DocentID = (int)reader["DocentID"];
                    taak.DocentNaam = (string)reader["Naam"];
                    taak.Taak_id = (int)reader["Taak_id"];
                    GefixeerdeTaken.Add(taak);
                }
                return GefixeerdeTaken;
            }
            catch
            {
                return null;
            }
            finally
            {
                connectie.Close();
            }
        }

        public GefixeerdeTaak HaalGefixeerdeTaakOpMetID(int teamid)
        {
            GefixeerdeTaak taak = new GefixeerdeTaak();
            try
            {
                connectie.Open();
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "SELECT F.*, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[GefixeerdeTaken] F INNER JOIN [dbo].[Docent] D ON F.DocentID = D.DocentID INNER JOIN [dbo].[AspNetUsers] ANU ON D.MedewerkerID = ANU.Id INNER JOIN [dbo].[TeamLeider] TL ON D.DocentID=TL.MedewerkerID INNER JOIN [dbo].[Team] T ON TL.TeamleiderID=T.TeamLeiderID WHERE T.TeamID = " + teamid + ")";
                //TODO SELECT F.*, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[GefixeerdeTaken] F INNER JOIN [dbo].[Docent] D ON F.DocentID = D.DocentID INNER JOIN [dbo].[AspNetUsers] ANU ON D.MedewerkerID = ANU.Id INNER JOIN [dbo].[TeamLeider] TL ON D.DocentID=TL.MedewerkerID INNER JOIN [dbo].[Team] T ON TL.TeamleiderID=T.TeamLeiderID WHERE T.TeamID = 5
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    taak.Fix_id = (int)reader["Fix_id"];
                    taak.DocentID = (int)reader["DocentID"];
                    taak.DocentNaam = (string)reader["Naam"];
                    taak.Taak_id = (int)reader["Taak_id"];
                }
                return taak;
            }
            catch
            {
                // TODO: Exception Handling!
                return null;
            }
            finally
            {
                connectie.Close();
            }
        }
    }
}
