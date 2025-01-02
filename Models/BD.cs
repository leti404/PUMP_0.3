using System.Data.SqlClient;
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
                    string sql = "select concat(CAST(STMPDH_DESCRP AS NVARCHAR(MAX)),'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_DESCRP  IS NOT NULL) And ((STMPDH_DESCRP COLLATE Latin1_General_CI_AI Like '@TextoBusqueda' )  or ( USR_STMPDH_NOTTEC  COLLATE Latin1_General_CI_AI Like '@TextoBusqueda' ))  ORDER BY CAST( STMPDH_DESCRP  AS NVARCHAR(MAX)) ASC ";
                    _listaNombreProductos = PUMPCO.Query<string>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 2:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_ARTCOD  IS NOT NULL)AND STMPDH_TIPPRO = '@Codigo' And (replace(replace ((STMPDH_ARTCOD COLLATE Latin1_General_CI_AI),'/',''),'-','' )Like '@TextoBusqueda' )  ORDER BY CAST( STMPDH_ARTCOD  AS NVARCHAR(MAX)) ASC ";
                    PUMPCO.Execute(sql, new{Codigo = codigo, TextoBusqueda = textoBusqueda}); 
                    _listaNombreProductos = PUMPCO.Query<string>(sql).ToList();
                }
            break;

            case 3:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(CAST(STMPDH_INDCOD AS NVARCHAR(MAX)),'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_INDCOD  IS NOT NULL) And (STMPDH_INDCOD COLLATE Latin1_General_CI_AI Like '@TextoBusqueda' )  ORDER BY CAST( STMPDH_INDCOD  AS NVARCHAR(MAX)) ASC ";
                    PUMPCO.Execute(sql, new{TextoBusqueda = textoBusqueda}); 
                    _listaNombreProductos = PUMPCO.Query<string>(sql).ToList();
                }
            break;

            case 4:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(CAST(USR_STMPDH_CODFAB AS NVARCHAR(MAX)),'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (USR_STMPDH_CODFAB  IS NOT NULL) And (USR_STMPDH_CODFAB COLLATE Latin1_General_CI_AI Like '@TextoBusqueda' )  ORDER BY CAST( USR_STMPDH_CODFAB  AS NVARCHAR(MAX)) ASC ";
                    PUMPCO.Execute(sql, new{TextoBusqueda = textoBusqueda}); 
                    _listaNombreProductos = PUMPCO.Query<string>(sql).ToList();
                }
            break;

            case 5:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "select concat(USR_CODFAB_DESCRI ,'- ', CAST(STMPDH_DESCRP AS NVARCHAR(MAX)) ,'- ', STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH inner join USR_CODFAB  on USR_STMPDH_FABRIC = USR_CODFAB_CODIGO  where (USR_CODFAB_DESCRI  IS NOT NULL) And (USR_CODFAB_DESCRI COLLATE Latin1_General_CI_AI Like '@TextoBusqueda' )  ORDER BY CAST( USR_CODFAB_DESCRI  AS NVARCHAR(MAX)) ASC";
                    PUMPCO.Execute(sql, new{TextoBusqueda = textoBusqueda}); 
                    _listaNombreProductos = PUMPCO.Query<string>(sql).ToList();
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
    public Producto ObtenerInfoCompletProdu(string codigo)
    {
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = "SELECT P.STMPDH_ARTCOD AS CODIGO, P.STMPDH_INDCOD AS CODREDUCIDO, P.STMPDH_DESCRP AS DESCRIPCION, P.USR_STMPDH_CODFAB AS CODIGOFABRICANTE, P.STMPDH_TIPPRO AS TIPO, P.USR_STMPDH_NOTTEC AS NOMBFAB, F.USR_CODFAB_DESCRI AS CODIFAB, P.STMPDH_BMPBMP AS FOTO, P.USR_STMPDH_NOHABI AS APTOVENTA, SUM(S.STRMVK_STOCKS) AS STOCK FROM STMPDH AS P LEFT JOIN USR_CODFAB AS F ON P.USR_STMPDH_FABRIC = F.USR_CODFAB_CODIGO LEFT JOIN STRMVK AS S ON CAST(P.STMPDH_ARTCOD AS NVARCHAR(MAX)) = S.STRMVK_ARTCOD WHERE P.STMPDH_ARTCOD = '@Codigo'AND S.STRMVK_DEPOSI = 01 AND S.STRMVK_SECTOR = 0 GROUP BY P.STMPDH_ARTCOD, P.STMPDH_INDCOD, P.STMPDH_DESCRP, P.USR_STMPDH_CODFAB, P.STMPDH_TIPPRO, P.USR_STMPDH_NOTTEC, F.USR_CODFAB_DESCRI, P.STMPDH_BMPBMP, P.USR_STMPDH_NOHABI";
            PUMPCO.Execute(sql, new{Codigo = codigo}); 
            productoUsuario = PUMPCO.QueryFirstOrDefault<Producto>(sql, new { Codigo = codigo });
        }
        return productoUsuario;
    }
}