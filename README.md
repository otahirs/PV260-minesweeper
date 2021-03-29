# MineSweeper Class Diagram

![alt text](pv260_hw1_normal.png "MineSweeper Class Diagram")  


# Úkol 1 - TDD

S využitím TDD naprogramujte hru "Hledání min".

1) Utvořte trojice.
2) Forkněte si tento repozitář.
3) Společně naimplementuje rešení a změřte si čas, jak dlouho jste příklad řešili. Pokud se dostanete nad 6 hodin, dejte mi vědět.
4) Dělejte časté commity ať je vidět, jak jste postupovali.
5) Odkaz na vaše repo dejte do Miro boardu.
6) Implementujte jenom business logiku hry bez toho, abyste vypisovali cokoli na konzoli nebo jiný výstup (typ aplikace bude class library).
7) Soustřeďte se na business pravidla, jejich zachycení v testech a objektový návrh.  
8) Cílem je mít kód, který se bude číst jako kniha. Současně s tím také procvičit rytmus TDD.  

[<img src="https://upload.wikimedia.org/wikipedia/commons/c/c8/Commonly_Found_Minesweeper_Theme.png">]

## Zadání

Herní pole je definováno svoji velikostí. Minimální velikost herního pole je 3x3, maximální 50x50.
Hra vygeneruje herní pole o dané velikosti a náhodně na něj umístí miny.
Min je vždy minimálně 20% a maximálně 60% ze všech možných polí. (Tzn. u hracího pole 10x10 může být minimálně 20 a maximálně 60 min).

Následně bude hra dostávat jako vstupy souřadnice polí, které hráč "šlápl" nebo které označil vlajkou.

Příklad:  
1 2 znamená, že uživatel vybral pole v prvním řádku a druhém sloupci.
Pokud šlápl na minu, hra skončila.
Pokud označil pole vedle miny, vrátí se mu herní pole s odkrytým číslem, které symbolizuje s kolika minami dané pole sousedí.
Pokud označil pole, na kterém není mina a které ani s žádnou minou nesousedí, dostane herní pole s odkrytou největší možnou souvislou plochou.  
Příklad + nápověda:   
(pro lepí čitelnost si otevřete tento soubor v editoru s nejakym monospaced fontem)  
Z herního pole (kde "x" označuje polohu miny a "." neodkryté pole)  
x...  
....  
.x..  
....  

si spočítejte následující matici   

x100  
2210  
1x10  
1110  

Pokud hráč zadá 1 2, vrátí se mu následující

.1..  
....  
....  
.... 

Pokud hráč zadá 1 3, vrátí se mu herní pole které bude vypadat takto

.100  
..10  
..10  
..10  

V případě, že matice herního pole vypadá následovně (a uživatel zatím nemá žádná odkrytá pole)

x100  
1111  
001x  
0011 

a pokud uživatel zadá 3 1, vrátí se mu následující  
....  
111.  
001.  
001. 

Hráč má možnost si jednotlivá pole, na kterých si myslí že je mina, označit vlajkou.
Hra končí, pokud jsou všechny miny označeny.
