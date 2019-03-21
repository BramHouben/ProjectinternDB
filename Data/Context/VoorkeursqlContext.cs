﻿using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data.Context
{
    internal class VoorkeurSQLContext : IVoorkeurContext
    {
        private SqlConnection connectie;

        private DBconn dbconn = new DBconn();

        public List<Voorkeur> VoorkeurenOphalen()
        {
            List<Voorkeur> vklistmodel = new List<Voorkeur>();

            connectie = dbconn.GetConnString();
            connectie.Open();

            var cmd = new SqlCommand("SELECT * FROM Voorkeur", connectie);
            var reader = cmd.ExecuteReader();

         

            while (reader.Read())
            {
                var voorkeur = new Voorkeur();


                voorkeur.Id = (int)reader["Id"];
                voorkeur.Vak_naam = (string)reader["vak_naam"];
                voorkeur.Prioriteit = (int)reader["Prioriteit"]
                ;

                vklistmodel.Add(voorkeur);
            }

            connectie.Close();

            return vklistmodel;
        }

     

        public void VoorkeurToevoegen(Voorkeur voorkeur)
        {
        
            try
            {
                connectie = dbconn.GetConnString();

                connectie.Open();
                var command = connectie.CreateCommand();
                command.Parameters.AddWithValue("@vak_naam", voorkeur.Vak_naam);
                command.Parameters.AddWithValue("@Prioriteit", voorkeur.Prioriteit);

                command.CommandText = "INSERT INTO Voorkeur (Vak_naam, Prioriteit) VALUES ( @vak_naam, @Prioriteit)";
                command.ExecuteNonQuery();

            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                connectie.Close();
            }
        }
    }
}