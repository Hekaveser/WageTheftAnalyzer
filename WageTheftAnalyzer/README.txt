Funkce, které by kalkulaèka mohla mít:
Vstupní údaje:

Poèáteèní mzda a její zmìny za urèité období (napø. každoroèní zvýšení nebo snížení).
Roèní míra inflace (z historických dat nebo zadání ruènì).
Výpoèet reálné hodnoty mzdy:

Výpoèet reálného poklesu nebo rùstu mzdy po zohlednìní inflace. Uživatel by mohl vidìt, jak se mìní reálná hodnota mzdy v prùbìhu let.
Možnost zobrazit konkrétní rok nebo delší èasový úsek, aby vidìl kumulativní dopad inflace na mzdu.
Vizualizace výsledkù:

Grafické zobrazení rozdílu mezi nominální a reálnou mzdou v èase.
Pøehledné shrnutí, kolik reálnì „ztratil“ nebo „získal“ v porovnání se svou nominální mzdou.
Porovnání se státním prùmìrem:

Pokud by aplikace zahrnovala data o prùmìrné inflaci a zmìnì prùmìrných mezd v ÈR, uživatel by mohl vidìt, jak si stojí oproti prùmìru.
Tipy a doporuèení:

Na základì výsledkù by kalkulaèka mohla nabídnout tipy, jak se s inflací vyrovnat (napøíklad strategie úspor, tipy na vyjednávání mzdového zvýšení nebo informace o zajištìní proti inflaci).
Možné technické zpracování:
Backend: .NET Core API, které by zpracovávalo vstupy a poskytovalo výpoèty.
Frontend: Hezké a pøehledné uživatelské rozhraní, napøíklad v Angularu nebo Blazoru, které by nabídlo interaktivní grafy a tabulky.
Data: Míru inflace by mohla aplikace aktualizovat automaticky pøes dostupná API nebo zadat ruènì, pokud by uživatel preferoval vlastní hodnoty.


Data o inflaci pùjde asi nejsnadnìji získat z Eurostatu, a to zde:


https://ec.europa.eu/eurostat/web/query-builder/tool

dá se také získat míra inflace pro konkrétní oblast. Asi je vhodné toto cachovat mìsíc, pokud se ovšem podaøí získat inflaci mìsíènì.