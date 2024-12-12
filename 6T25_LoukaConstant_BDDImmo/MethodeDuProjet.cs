using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace _6T25_LoukaConstant_BDDImmo
{
    internal class MethodeDuProjet
    {
        public void CD()
        {
            MySqlConnection conn = null;
            MySqlDataAdapter adapter = null;
        }

        static string definirCheminBDD() // détermine la chaine de connexion
        {
            return "server=10.10.51.98;database=immo;port=3306;UserId=root;password=root";
        }

        public bool chercherUtilisateurs(out DataSet infos) // Chercher users dans la base de données
        {
            bool ok = false;
            MySqlConnection maConnection = new MySqlConnection(definirCheminBDD());
            string query = "";

            try
            {
                maConnection.Open();

                query = "SELECT * FROM utilisateurs;";

                MySqlDataAdapter da = new MySqlDataAdapter(query, maConnection);
                infos = new DataSet();
                da.Fill(infos, "infosUtilisateurs");
                maConnection.Close();

                if (infos.Tables[0].Rows.Count >= 1)
                {
                    ok = true;
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return ok;
        }

        public bool updateData(int bienId, string nom)
        {
            bool ok = false;
            MySqlConnection maConnection = new MySqlConnection(definirCheminBDD());
            string query = "";

            try
            {
                query = "UPDATE biens SET nom = @nom Where bienId =" + bienId + ";";
                maConnection.Open();
                MySqlCommand updateCommand = new MySqlCommand(query, maConnection);
                updateCommand.Parameters.AddWithValue("@nom", nom);
                
                if (updateCommand.ExecuteNonQuery() > 0)
                {
                    ok = true;
                }

                maConnection.Close();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return ok;

        }

        public bool AjouteUser(string[] donnees)
        {
            bool ok = false;
            MySqlConnection maConnection = new MySqlConnection(definirCheminBDD());
            string query = "";

            try
            {
                query = "INSERT INTO utilisateurs (nomUser, prenomUser, loginUser, passWordUser, role)  VALUES (@nomUser, @prenomUser, @loginUser, @passWordUser, @role)";
                maConnection.Open();
                MySqlCommand addCommand = new MySqlCommand(query, maConnection);
                addCommand.Parameters.AddWithValue("@nomUser", donnees[0]);
                addCommand.Parameters.AddWithValue("@prenomUser", donnees[1]);
                addCommand.Parameters.AddWithValue("@loginUser", donnees[2]);
                addCommand.Parameters.AddWithValue("@passWordUser", donnees[3]);
                addCommand.Parameters.AddWithValue("@role", donnees[4]);

                if (addCommand.ExecuteNonQuery() > 0)
                {
                    ok = true;
                }

                maConnection.Close();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return ok;
        }

        public bool SuppData(string user)
        {
            bool ok = false;
            MySqlConnection maconnection = new MySqlConnection();
            string query = "";

            try
            {
                query = "DELETE FROM utilisateurs WHERE nomUser =´" + user + "´";
                maconnection.Open();
                MySqlCommand suppCommand = new MySqlCommand(query, maconnection);
                
                if (suppCommand.ExecuteNonQuery() > 0)
                {
                    ok = true;
                }
                
                maconnection.Close();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return ok;
        }

        public string AfficheUser(DataSet infos)
        {
            string contenuDataset = "";

            for (int i = 0; i < infos.Tables[0].Rows.Count; i++)
            {
                contenuDataset += infos.Tables[0].Rows[0]["nomUser"].ToString() + " " + infos.Tables[0].Rows[0]["prenomUser"].ToString();
            }
;
            return contenuDataset;
        }
            

    }
}



    

