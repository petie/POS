insert into POS.dbo.Tax  (Symbol, Value, FiscalSymbol) SELECT [DSSVAT_Stawka], [DSSVAT_Stawka], [DSSVAT_DFSymbol]
  FROM [CDN_BIOVERT].[CDN].[DetalStanStawkiVAT] where DSSVAT_DFSymbol <> '' and DSSVAT_StanDetalID = 1

INSERT INTO Pos.[dbo].[Products]
           ([Ean]
           ,[Name]
           ,[Price]
           ,[Unit]
           ,[TaxId])
     SELECT [Twr_EAN]
	  ,[Twr_Nazwa]
      ,(select twc_wartosc from CDN_biovert.cdn.TwrCeny where TwC_TwCNumer = Twr_TwCNumer and twc_typ = 2 and TwC_TwrID = Twr_TwrId)
	  ,[Twr_JM]
      ,(select Id from pos.dbo.tax where value = [Twr_Stawka])
  FROM [CDN_BIOVERT].[CDN].[Towary] where Twr_NieAktywny = 0
