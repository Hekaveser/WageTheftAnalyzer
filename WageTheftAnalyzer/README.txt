Funkce, kter� by kalkula�ka mohla m�t:
Vstupn� �daje:

Po��te�n� mzda a jej� zm�ny za ur�it� obdob� (nap�. ka�doro�n� zv��en� nebo sn�en�).
Ro�n� m�ra inflace (z historick�ch dat nebo zad�n� ru�n�).
V�po�et re�ln� hodnoty mzdy:

V�po�et re�ln�ho poklesu nebo r�stu mzdy po zohledn�n� inflace. U�ivatel by mohl vid�t, jak se m�n� re�ln� hodnota mzdy v pr�b�hu let.
Mo�nost zobrazit konkr�tn� rok nebo del�� �asov� �sek, aby vid�l kumulativn� dopad inflace na mzdu.
Vizualizace v�sledk�:

Grafick� zobrazen� rozd�lu mezi nomin�ln� a re�lnou mzdou v �ase.
P�ehledn� shrnut�, kolik re�ln� �ztratil� nebo �z�skal� v porovn�n� se svou nomin�ln� mzdou.
Porovn�n� se st�tn�m pr�m�rem:

Pokud by aplikace zahrnovala data o pr�m�rn� inflaci a zm�n� pr�m�rn�ch mezd v �R, u�ivatel by mohl vid�t, jak si stoj� oproti pr�m�ru.
Tipy a doporu�en�:

Na z�klad� v�sledk� by kalkula�ka mohla nab�dnout tipy, jak se s inflac� vyrovnat (nap��klad strategie �spor, tipy na vyjedn�v�n� mzdov�ho zv��en� nebo informace o zaji�t�n� proti inflaci).
Mo�n� technick� zpracov�n�:
Backend: .NET Core API, kter� by zpracov�valo vstupy a poskytovalo v�po�ty.
Frontend: Hezk� a p�ehledn� u�ivatelsk� rozhran�, nap��klad v Angularu nebo Blazoru, kter� by nab�dlo interaktivn� grafy a tabulky.
Data: M�ru inflace by mohla aplikace aktualizovat automaticky p�es dostupn� API nebo zadat ru�n�, pokud by u�ivatel preferoval vlastn� hodnoty.


Data o inflaci p�jde asi nejsnadn�ji z�skat z Eurostatu, a to zde:


https://ec.europa.eu/eurostat/web/query-builder/tool

d� se tak� z�skat m�ra inflace pro konkr�tn� oblast. Asi je vhodn� toto cachovat m�s�c, pokud se ov�em poda�� z�skat inflaci m�s��n�.