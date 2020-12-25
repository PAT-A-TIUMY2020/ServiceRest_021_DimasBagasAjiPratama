using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace ServiceRest_021_Dimas_Bagas_AjiPratama
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        string dbstring = "Data Source=DESKTOP-RD7FIRP;Initial Catalog=\"TI UMY\";Integrated Security=True;";
        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            SqlConnection connection = new SqlConnection(dbstring);
            string query = String.Format("insert into dbo.Mahasiswa values ('{0}', '{1}', '{2}', '{3}')", mhs.nim, mhs.nama, mhs.prodi, mhs.angkatan);
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                connection.Close();
                msg = "SUKSES";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }

            return msg;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();

            SqlConnection connection = new SqlConnection(dbstring);
            string query = "Select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa ";
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mahas;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection connection = new SqlConnection(dbstring);
            string query = string.Format("select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }

            return mhs;

        }

        public string UpdateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";

            SqlConnection connection = new SqlConnection(dbstring);
            string query = String.Format("update dbo.Mahasiswa set Nama = '" + mhs.nama + "', Prodi = '" + mhs.prodi + "', Angkatan= '" + mhs.angkatan + "' where NIM = '" + mhs.nim + "'");
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                connection.Close();
                msg = "SUKSES";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }

            return msg;
        }
    }
}