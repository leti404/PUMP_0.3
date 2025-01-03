using Microsoft.Data.SqlClient;
using System;
using Dapper;
using System.Data;
public class BD{
    private static string _connectionString = @"Server=CONTROL\SQL_PUMP;database=PUMPCO;uid=Lectura;pwd=PublicUser102938*;Trusted_Connection=False;TrustServerCertificate=True;";
    public static List<string> _listaNombreProductos = new List<string>();
    public static List<string> ListaBusquedaNombProdu(int tipoBusqueda, string textoBusqueda, string codigo)
    {
        switch (tipoBusqueda)
        {
            case 1:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(CAST(STMPDH_DESCRP AS NVARCHAR(MAX)),'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_DESCRP  IS NOT NULL) And ((STMPDH_DESCRP COLLATE Latin1_General_CI_AI Like @TextoBusqueda )  or ( USR_STMPDH_NOTTEC  COLLATE Latin1_General_CI_AI Like @TextoBusqueda ))  ORDER BY CAST( STMPDH_DESCRP  AS NVARCHAR(MAX)) ASC ";
                    _listaNombreProductos = PUMPCO.Query<string>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 2:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_ARTCOD  IS NOT NULL)AND STMPDH_TIPPRO = @Codigo And (replace(replace ((STMPDH_ARTCOD COLLATE Latin1_General_CI_AI),'/',''),'-','' )Like @TextoBusqueda )  ORDER BY CAST( STMPDH_ARTCOD  AS NVARCHAR(MAX)) ASC "; 
                    _listaNombreProductos = PUMPCO.Query<string>(sql, new {Codigo = codigo, TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 3:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(CAST(STMPDH_INDCOD AS NVARCHAR(MAX)),'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_INDCOD  IS NOT NULL) And (STMPDH_INDCOD COLLATE Latin1_General_CI_AI Like @TextoBusqueda )  ORDER BY CAST( STMPDH_INDCOD  AS NVARCHAR(MAX)) ASC ";
                    _listaNombreProductos = PUMPCO.Query<string>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 4:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(CAST(USR_STMPDH_CODFAB AS NVARCHAR(MAX)),'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (USR_STMPDH_CODFAB  IS NOT NULL) And (USR_STMPDH_CODFAB COLLATE Latin1_General_CI_AI Like @TextoBusqueda )  ORDER BY CAST( USR_STMPDH_CODFAB  AS NVARCHAR(MAX)) ASC ";
                    _listaNombreProductos = PUMPCO.Query<string>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 5:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(USR_CODFAB_DESCRI ,'- ', CAST(STMPDH_DESCRP AS NVARCHAR(MAX)) ,'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH inner join USR_CODFAB  on USR_STMPDH_FABRIC = USR_CODFAB_CODIGO  where (USR_CODFAB_DESCRI  IS NOT NULL) And (USR_CODFAB_DESCRI COLLATE Latin1_General_CI_AI Like @TextoBusqueda )  ORDER BY CAST( USR_CODFAB_DESCRI  AS NVARCHAR(MAX)) ASC";
                    _listaNombreProductos = PUMPCO.Query<string>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;
        }
        return _listaNombreProductos;
    }

    public static List<SeguimeintoFormula> _listaSeguimeintoFromula = new List<SeguimeintoFormula>();
    //public static List<SeguimeintoFormula> SeguimeintoFormula()
    //{
    //    using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
    //    {
    //        string sql = "select concat(STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_ARTCOD  IS NOT NULL)AND STMPDH_TIPPRO = '@Codigo' And (replace(replace ((STMPDH_ARTCOD COLLATE Latin1_General_CI_AI),'/',''),'-','' )Like '@TextoBusqueda' )  ORDER BY CAST( STMPDH_ARTCOD  AS NVARCHAR(MAX)) ASC ";
    //        PUMPCO.Execute(sql, new{Codigo = codigo, TextoBusqueda = textoBusqueda}); 
    //        _listaNombreProductos = PUMPCO.Query<string>(sql).ToList();
    //    }
    //    return _listaSeguimeintoFromula;
    //}

    public static List<string> _listaCharTipoCod = new List<string>();
    public static List<string> ObtenerListaCodigos()
    {
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = "select STMPDH_TIPPRO as Nombre from STMPDH group by STMPDH_TIPPRO order by STMPDH_TIPPRO";
            _listaCharTipoCod = PUMPCO.Query<string>(sql).ToList();
        }
        return _listaCharTipoCod;
    }
    public static Producto productoUsuario;
    public static Producto ObtenerInfoCompletProdu(string codigo, string miniCod)
    {
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = @"SELECT STMPDH.STMPDH_ARTCOD AS CODIGO, STMPDH.STMPDH_DESCRP AS DESCRIPCION, STMPDH.USR_STMPDH_CODFAB AS CODFAB, STMPDH.STMPDH_TIPPRO AS TIPO, STMPDH.USR_STMPDH_NOTTEC AS DESCADI, USR_CODFAB.USR_CODFAB_DESCRI AS NOMBFABRI, STMPDH.STMPDH_BMPBMP AS FOTO, STMPDH.USR_STMPDH_NOHABI AS APTOVENTA, SUM(STRMVK.STRMVK_STOCKS) AS STOCK FROM STMPDH LEFT JOIN USR_CODFAB ON STMPDH.USR_STMPDH_FABRIC = USR_CODFAB.USR_CODFAB_CODIGO LEFT JOIN STRMVK ON CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = STRMVK.STRMVK_ARTCOD WHERE CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = '" + codigo + "' AND STMPDH.STMPDH_TIPPRO = '" + miniCod + "'AND STRMVK.STRMVK_DEPOSI = 01AND STRMVK.STRMVK_SECTOR = 0 GROUP BY STMPDH.STMPDH_ARTCOD, STMPDH.STMPDH_DESCRP, STMPDH.USR_STMPDH_CODFAB, STMPDH.STMPDH_TIPPRO, STMPDH.USR_STMPDH_NOTTEC, USR_CODFAB.USR_CODFAB_DESCRI, STMPDH.STMPDH_BMPBMP, STMPDH.USR_STMPDH_NOHABI ORDER BY STMPDH.STMPDH_ARTCOD DESC";
            productoUsuario = PUMPCO.QueryFirstOrDefault<Producto>(sql);
        }
        return productoUsuario;
    }
}