using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private string CadenaConexion;
        public UsuarioRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        public Task<bool> ActualizarAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"UPDATE usuario SET Nombre = @Nombre, Contrasena = @Contrasena, Correo = @Correo, Rol = @Rol, Foto = @Foto, EstaActivo = @EstaActivo
                                WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public Task<bool> EliminarAsync(string Codigo)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "DELETE FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { codigo }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public Task<IEnumerable<Usuario>> GetListAsync()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario;";
                lista = await _conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public Task<Usuario> GetPorCodigoUsuario(string Codigo)
        {
            Usuario user = new Usuario();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
                user = await _conexion.QueryFirstAsync<Usuario>(sql, new { codigo });
            }
            catch (Exception)
            {
            }
            return user;
        }

        public Task<bool> NuevoAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }





    }
}
