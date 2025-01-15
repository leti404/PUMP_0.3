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
                    string sql = "select concat(STMPDH_TIPPRO ,'- ', CAST(STMPDH_ARTCOD AS NVARCHAR(MAX))) AS Nombre from STMPDH where (STMPDH_ARTCOD  IS NOT NULL)AND STMPDH_TIPPRO LIKE @Codigo And (replace(replace ((STMPDH_ARTCOD COLLATE Latin1_General_CI_AI),'/',''),'-','' )Like @TextoBusqueda )  ORDER BY CAST( STMPDH_ARTCOD  AS NVARCHAR(MAX)) ASC "; 
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

    public static List<SeguimientoFormula> _listaSeguimeintoFromula = new List<SeguimientoFormula>();
    public static List<SeguimientoFormula> SeguimientoFormula()
    {
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = "SELECT STTFOI.STTFOI_TIPPRO AS 'TIPO', STTFOI.STTFOI_ARTCOD AS 'CODIGO', STTFOI.STTFOI_FORMUL AS 'FORMULA', STTFOI.STTFOI_UNIITM AS 'UNIDAD', STTFOI.STTFOI_CNTNOM AS 'CANTIDAD', STMPDH.STMPDH_DESCRP AS 'DESCRIPCION', COALESCE((SELECT SUM(STRMVK.STRMVK_STOCKS) FROM STRMVK WHERE STRMVK.STRMVK_DEPOSI = 01 AND STRMVK.STRMVK_SECTOR = 0 AND STRMVK.STRMVK_TIPPRO = STTFOI.STTFOI_TIPPRO AND STRMVK.STRMVK_ARTCOD = STTFOI.STTFOI_ARTCOD), 0) AS 'STOCK' FROM  STTFOI INNER JOIN  STMPDH  ON  STMPDH.STMPDH_TIPPRO = STTFOI.STTFOI_TIPPRO  AND STMPDH.STMPDH_ARTCOD = STTFOI.STTFOI_ARTCOD WHERE  STTFOI.STTFOI_ARTITM = '" + productoUsuario.CODIGO + "'  AND STTFOI.STTFOI_TIPITM = '" + productoUsuario.TIPO + "' ORDER BY  STTFOI.STTFOI_ARTCOD";
            _listaSeguimeintoFromula = PUMPCO.Query<SeguimientoFormula>(sql).ToList();

        }
        return _listaSeguimeintoFromula;
    }

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

    public static List<string> ObtenerFabriListaBusqueda(string textoBusqueda)
    {
        List<string> fabri = new List<string>();
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = @"select USR_CODFAB_DESCRI AS Nombre from STMPDH inner join USR_CODFAB  on USR_STMPDH_FABRIC = USR_CODFAB_CODIGO  where (USR_CODFAB_DESCRI  IS NOT NULL) And (USR_CODFAB_DESCRI COLLATE Latin1_General_CI_AI Like '%" + textoBusqueda +"%' ) Group by USR_CODFAB_DESCRI   ORDER BY CAST( USR_CODFAB_DESCRI  AS NVARCHAR(MAX)) ASC ";
            fabri = PUMPCO.Query<string>(sql).ToList();
        }
        return fabri;
    }

    public static List<string> ObtenerListaFormula(string codigo, string miniCod)
    {
        List<string> listaFormula = new List<string>();
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = @"select concat (STTFOH_FORMUL ,' - ', CONVERT(VARCHAR(10), STTFOH_FCHFIN, 120)) as 'Formula'  from STTFOH where STTFOh_ARTCOD = '" + codigo + "' and STTFOh_TIPPRO ='" + miniCod + "'";
            listaFormula = PUMPCO.Query<string>(sql).ToList();
        }
        return listaFormula;
    }

    public static List<Formula> ObtenerComponentesFormula(string codigo, string miniCod, int multiplicador, string formula1)
    {
        List<Formula> listaFormula = new List<Formula>();
        using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
        {
            string sql = @"SELECT STTFOI.STTFOI_TIPITM AS 'TIPO', STTFOI.STTFOI_ARTITM AS 'CODIGO', STTFOI.STTFOI_FORMUI AS 'FORMULA', STTFOI.STTFOI_UNIITM AS 'UNIDAD', STTFOI.STTFOI_CNTNOM AS 'CANTIDAD', STMPDH.STMPDH_DESCRP AS 'DESCRIPCION', STTFOI.USR_STTFOI_REFERE AS 'REFERENCIA', STTFOI.STTFOI_CNTNOM * @Multiplicador AS 'TOTAL', ISNULL(( SELECT SUM(STRMVK.STRMVK_STOCKS)  FROM STRMVK  WHERE STRMVK.STRMVK_DEPOSI = 1  AND STRMVK.STRMVK_SECTOR = 0  AND STRMVK.STRMVK_TIPPRO = STTFOI.STTFOI_TIPITM  AND STRMVK.STRMVK_ARTCOD = STTFOI.STTFOI_ARTITM ), 0) AS 'STOCK',  ISNULL(( SELECT SUM(STRMVK.STRMVK_STOCKS)  FROM STRMVK  WHERE STRMVK.STRMVK_DEPOSI = 1  AND STRMVK.STRMVK_SECTOR = 0  AND STRMVK.STRMVK_TIPPRO = STTFOI.STTFOI_TIPITM  AND STRMVK.STRMVK_ARTCOD = STTFOI.STTFOI_ARTITM ), 0) - (STTFOI.STTFOI_CNTNOM * @Multiplicador) AS 'DIFERENCIA'  FROM  STTFOI INNER JOIN  STMPDH  ON STMPDH.STMPDH_TIPPRO = STTFOI.STTFOI_TIPITM  AND STMPDH.STMPDH_ARTCOD = STTFOI.STTFOI_ARTITM WHERE STTFOI.STTFOI_ARTCOD = '" + codigo + "' AND STTFOI.STTFOI_TIPPRO = '" + miniCod + "' AND STTFOI.STTFOI_FORMUL = @Formula1 ORDER BY STTFOI.STTFOI_FORMUL;";
            listaFormula = PUMPCO.Query<Formula>(sql, new {Multiplicador = multiplicador, Formula1 = formula1}).ToList();
        }
        return listaFormula;
    }

    public static List<Tabla> _listaProduTabla = new List<Tabla>();
    public static List<Tabla> ObtenerItemsTabla(int tipoBusqueda, string textoBusqueda, string codigo)
    {
        switch (tipoBusqueda)
        {
            case 1:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT STMPDH.STMPDH_ARTCOD AS CODIGO, STMPDH.STMPDH_DESCRP AS DESCRIPCION, STMPDH.USR_STMPDH_CODFAB AS CODFAB, STMPDH.STMPDH_TIPPRO AS TIPO, STMPDH.USR_STMPDH_NOTTEC AS DESCADI, USR_CODFAB.USR_CODFAB_DESCRI AS NOMBFABRI, STMPDH.STMPDH_BMPBMP AS FOTO, STMPDH.USR_STMPDH_NOHABI AS APTOVENTA, SUM(STRMVK.STRMVK_STOCKS) AS STOCK FROM STMPDH LEFT JOIN USR_CODFAB ON STMPDH.USR_STMPDH_FABRIC = USR_CODFAB.USR_CODFAB_CODIGO LEFT JOIN STRMVK ON CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = STRMVK.STRMVK_ARTCOD WHERE STMPDH.STMPDH_DESCRP IS NOT NULL AND (STMPDH.STMPDH_DESCRP COLLATE Latin1_General_CI_AI LIKE @TextoBusqueda OR STMPDH.USR_STMPDH_NOTTEC COLLATE Latin1_General_CI_AI LIKE @TextoBusqueda) GROUP BY STMPDH.STMPDH_ARTCOD, STMPDH.STMPDH_DESCRP, STMPDH.USR_STMPDH_CODFAB, STMPDH.STMPDH_TIPPRO, STMPDH.USR_STMPDH_NOTTEC, USR_CODFAB.USR_CODFAB_DESCRI, STMPDH.STMPDH_BMPBMP, STMPDH.USR_STMPDH_NOHABI ORDER BY STMPDH.STMPDH_DESCRP ASC";
                    _listaProduTabla = PUMPCO.Query<Tabla>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 2:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT STMPDH.STMPDH_ARTCOD AS CODIGO, STMPDH.STMPDH_DESCRP AS DESCRIPCION, STMPDH.USR_STMPDH_CODFAB AS CODFAB, STMPDH.STMPDH_TIPPRO AS TIPO, STMPDH.USR_STMPDH_NOTTEC AS DESCADI, USR_CODFAB.USR_CODFAB_DESCRI AS NOMBFABRI, STMPDH.STMPDH_BMPBMP AS FOTO, STMPDH.USR_STMPDH_NOHABI AS APTOVENTA, SUM(STRMVK.STRMVK_STOCKS) AS STOCK FROM STMPDH LEFT JOIN USR_CODFAB ON STMPDH.USR_STMPDH_FABRIC = USR_CODFAB.USR_CODFAB_CODIGO LEFT JOIN STRMVK ON CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = STRMVK.STRMVK_ARTCOD WHERE STMPDH.STMPDH_ARTCOD IS NOT NULL AND STMPDH.STMPDH_TIPPRO LIKE @Codigo AND (REPLACE(REPLACE(STMPDH.STMPDH_ARTCOD COLLATE Latin1_General_CI_AI, '/', ''), '-', '') LIKE @TextoBusqueda) GROUP BY STMPDH.STMPDH_ARTCOD, STMPDH.STMPDH_DESCRP, STMPDH.USR_STMPDH_CODFAB, STMPDH.STMPDH_TIPPRO, STMPDH.USR_STMPDH_NOTTEC, USR_CODFAB.USR_CODFAB_DESCRI, STMPDH.STMPDH_BMPBMP, STMPDH.USR_STMPDH_NOHABI ORDER BY STMPDH.STMPDH_ARTCOD ASC;"; 
                    _listaProduTabla = PUMPCO.Query<Tabla>(sql, new {Codigo = codigo, TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 3:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT STMPDH.STMPDH_ARTCOD AS CODIGO, STMPDH.STMPDH_DESCRP AS DESCRIPCION, STMPDH.USR_STMPDH_CODFAB AS CODFAB, STMPDH.STMPDH_TIPPRO AS TIPO, STMPDH.USR_STMPDH_NOTTEC AS DESCADI, USR_CODFAB.USR_CODFAB_DESCRI AS NOMBFABRI, STMPDH.STMPDH_BMPBMP AS FOTO, STMPDH.USR_STMPDH_NOHABI AS APTOVENTA, STMPDH.STMPDH_INDCOD AS INDCOD, SUM(STRMVK.STRMVK_STOCKS) AS STOCK FROM STMPDH LEFT JOIN USR_CODFAB ON STMPDH.USR_STMPDH_FABRIC = USR_CODFAB.USR_CODFAB_CODIGO LEFT JOIN STRMVK ON CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = STRMVK.STRMVK_ARTCOD WHERE  STMPDH.STMPDH_INDCOD IS NOT NULL AND (STMPDH.STMPDH_INDCOD COLLATE Latin1_General_CI_AI LIKE @TextoBusqueda) GROUP BY STMPDH.STMPDH_ARTCOD, STMPDH.STMPDH_DESCRP, STMPDH.USR_STMPDH_CODFAB, STMPDH.STMPDH_TIPPRO, STMPDH.USR_STMPDH_NOTTEC, USR_CODFAB.USR_CODFAB_DESCRI, STMPDH.STMPDH_BMPBMP, STMPDH.USR_STMPDH_NOHABI,STMPDH.STMPDH_INDCOD  ORDER BY STMPDH.STMPDH_INDCOD ASC;";
                    _listaProduTabla = PUMPCO.Query<Tabla>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 4:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT STMPDH.STMPDH_ARTCOD AS CODIGO, STMPDH.STMPDH_DESCRP AS DESCRIPCION, STMPDH.USR_STMPDH_CODFAB AS CODFAB, STMPDH.STMPDH_TIPPRO AS TIPO, STMPDH.USR_STMPDH_NOTTEC AS DESCADI, USR_CODFAB.USR_CODFAB_DESCRI AS NOMBFABRI, STMPDH.STMPDH_BMPBMP AS FOTO, STMPDH.USR_STMPDH_NOHABI AS APTOVENTA, SUM(STRMVK.STRMVK_STOCKS) AS STOCK FROM STMPDH LEFT JOIN USR_CODFAB ON STMPDH.USR_STMPDH_FABRIC = USR_CODFAB.USR_CODFAB_CODIGO LEFT JOIN STRMVK ON CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = STRMVK.STRMVK_ARTCOD WHERE STMPDH.USR_STMPDH_CODFAB IS NOT NULL AND (STMPDH.USR_STMPDH_CODFAB COLLATE Latin1_General_CI_AI LIKE @TextoBusqueda) GROUP BY STMPDH.STMPDH_ARTCOD, STMPDH.STMPDH_DESCRP, STMPDH.USR_STMPDH_CODFAB, STMPDH.STMPDH_TIPPRO, STMPDH.USR_STMPDH_NOTTEC, USR_CODFAB.USR_CODFAB_DESCRI, STMPDH.STMPDH_BMPBMP, STMPDH.USR_STMPDH_NOHABI ORDER BY STMPDH.USR_STMPDH_CODFAB ASC";
                    _listaProduTabla = PUMPCO.Query<Tabla>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;

            case 5:
                using(SqlConnection PUMPCO = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT STMPDH.STMPDH_ARTCOD AS CODIGO, STMPDH.STMPDH_DESCRP AS DESCRIPCION, STMPDH.USR_STMPDH_CODFAB AS CODFAB, STMPDH.STMPDH_TIPPRO AS TIPO, STMPDH.USR_STMPDH_NOTTEC AS DESCADI, USR_CODFAB.USR_CODFAB_DESCRI AS NOMBFABRI, STMPDH.STMPDH_BMPBMP AS FOTO, STMPDH.USR_STMPDH_NOHABI AS APTOVENTA, SUM(STRMVK.STRMVK_STOCKS) AS STOCK FROM STMPDH LEFT JOIN USR_CODFAB ON STMPDH.USR_STMPDH_FABRIC = USR_CODFAB.USR_CODFAB_CODIGO LEFT JOIN STRMVK ON CAST(STMPDH.STMPDH_ARTCOD AS NVARCHAR(MAX)) = STRMVK.STRMVK_ARTCOD WHERE USR_CODFAB.USR_CODFAB_DESCRI IS NOT NULL AND (USR_CODFAB.USR_CODFAB_DESCRI COLLATE Latin1_General_CI_AI LIKE @TextoBusqueda) GROUP BY STMPDH.STMPDH_ARTCOD, STMPDH.STMPDH_DESCRP, STMPDH.USR_STMPDH_CODFAB, STMPDH.STMPDH_TIPPRO, STMPDH.USR_STMPDH_NOTTEC, USR_CODFAB.USR_CODFAB_DESCRI, STMPDH.STMPDH_BMPBMP, STMPDH.USR_STMPDH_NOHABI ORDER BY USR_CODFAB.USR_CODFAB_DESCRI ASC;";
                    _listaProduTabla = PUMPCO.Query<Tabla>(sql, new {TextoBusqueda = textoBusqueda}).ToList();
                }
            break;
        }
        return _listaProduTabla;
    }
}