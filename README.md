# Gra strategiczna RPG

> Turowa gra strategiczna RPG, w której taktyka, rozwój bohaterów i odpowiednie przygotowanie drużyny decydują o zwycięstwie.

##  O projekcie

Projekt zakłada stworzenie turowej gry strategicznej RPG.
Gracz będzie dowodził drużyną bohaterów, która zmierzy się z kolejnymi falami przeciwników na planszach zbudowanych z pól heksagonalnych. Kluczowym elementem rozgrywki będzie odpowiednie planowanie ruchów, zarządzanie dostępnymi akcjami oraz wykorzystywanie umiejętności i wyposażenia postaci.
Gra będzie oferować system rozwoju bohaterów, ekwipunek, sklepy, ulepszanie przedmiotów oraz interakcje z otoczeniem. Przeciwnicy będą posiadać zróżnicowane statystyki, umiejętności i zachowania, wymagające od gracza dostosowywania strategii do aktualnej sytuacji.

##  Główne założenia rozgrywki

- Turowy system walki – planowanie działań i wykorzystywanie ograniczonej liczby akcji podczas tury,
- Plansza heksagonalna – taktyczne przemieszczanie się po mapie z uwzględnieniem pozycji bohaterów i przeciwników,
- System inicjatywy – ustalanie kolejności wykonywania tur na podstawie statystyk postaci, w szczególności `speed`,
- Rozwój bohaterów – zwiększanie statystyk i zdobywanie nowych możliwości,
- Ekwipunek – zdobywanie, wymienianie i ulepszanie wyposażenia,
- Umiejętności – wykorzystywanie indywidualnych zdolności postaci,
- AI przeciwników – przeciwnicy podejmujący decyzje na podstawie sytuacji na planszy,
- Eksploracja – generowane mapy, interakcje z otoczeniem i kolejne poziomy trudności

##  Planowane funkcjonalności:

System walki
- Turowy system walki,
- Kolejność tur zależna od statystyki szybkości (speed),
- Ograniczona liczba akcji dostępnych podczas tury,
- Atakowanie przeciwników,
- System statystyk i obrażeń,
- Umiejętności bohaterów,
- Różne stany postaci i przeciwników,
- SI przeciwników

System map i eksploracji
- Plansze zbudowane z heksagonalnych pól,
- Poruszanie się postaci po planszy,
- Proceduralne lub losowe generowanie map,
- Zapisywanie wygenerowanych map w celu ich późniejszego odtworzenia,
- Interakcje przedmiotów z otoczeniem,
- Losowe przedmioty i struktury na mapach,
- Przechodzenie na kolejne mapy o rosnącym poziomie trudności

System postaci i drużyny
- Obsługa wielu grywalnych postaci,
- Tworzenie i zarządzanie drużyną,
- System statystyk bohaterów,
- Rozwój postaci,
- System umiejętności,
- Różne typy bohaterów

Ekwipunek i przedmioty
- System ekwipunku,
- Różne rodzaje przedmiotów i wyposażenia,
- Losowanie wyposażenia przeciwników,
- Sklepy umożliwiające zakup przedmiotów,
- Ulepszanie posiadanego wyposażenia,
- Wykorzystanie przedmiotów podczas eksploracji

System przeciwników
- Różnorodne typy przeciwników,
- Indywidualne statystyki i możliwości,
- Sztuczna inteligencja sterująca zachowaniem przeciwników,
- Różne stany i zachowania jednostek

System użytkowników i zapisów
- Logowanie użytkowników,
- Przechowywanie danych w bazie danych,
- Zapisywanie i wczytywanie postępu gry,
- Izolacja danych poszczególnych użytkowników,
- Odtwarzanie wcześniej wygenerowanych map

Dodatkowe elementy
- Minigry,
- NPC,
- Animacje i efekty wizualne,
- Muzyka i efekty dźwiękowe,
- Opcje personalizacji

## Priorytety realizacji

> Projekt będzie rozwijany etapami, zgodnie z priorytetami poszczególnych funkcjonalności,

| Priorytet | Zakres |
- | Wysoki | Poruszanie się po planszy, system tur i akcji, statystyki, walka, AI, generowanie map, ekwipunek, umiejętności, sklepy i rozwój postaci |
- | Średni | Interakcje ze środowiskiem, stany postaci, zapisy gry, baza danych, optymalizacja, animacje, różne typy bohaterów i losowe elementy map |
- | Niski | Minigry hazardowe, muzyka, efekty dźwiękowe i wizualne, NPC oraz personalizacja |

##  Założenia techniczne

>Projekt będzie rozwijany z uwzględnieniem następujących założeń:

- Modularna architektura – podział kodu na logiczne i możliwie niezależne systemy,
- Rozszerzalność – możliwość dodawania nowych bohaterów, przeciwników, przedmiotów i umiejętności,
- Wydajność – płynne działanie podczas standardowej rozgrywki,
- Obsługa błędów – poprawna walidacja danych wejściowych i logowania,
- Bezpieczeństwo – ochrona danych użytkowników przed dostępem innych graczy,
- Trwałość danych – przechowywanie zapisów w bazie danych i możliwość odtworzenia stanu rozgrywki

>Szczegółowa dokumentacja techniczna będzie uzupełniana w trakcie implementacji.

>Dokumentacja będzie rozwijana równolegle z kodem i aktualizowana wraz ze zmianami w poszczególnych modułach.

##  Status projektu

>Projekt w trakcie tworzenia – poniższa lista przedstawia docelowy zakres prac.

-  System planszy heksagonalnej,
-  Poruszanie postaci,
-  System tur i inicjatywy,
-  System akcji,
-  System walki,
-  Statystyki postaci,
-  SI przeciwników,
-  Generowanie map,
-  System ekwipunku,
-  System umiejętności,
-  Rozwój bohaterów,
-  System sklepów i ulepszania wyposażenia,
-  Interakcje z otoczeniem,
-  System drużyny,
-  System zapisów i wczytywania gry,
-  Baza danych i logowanie,
-  Kolejne poziomy trudności,
-  Dodatkowe aktywności i minigry

##  Kierunek rozwoju

Docelowo gra ma łączyć taktyczną walkę na planszy heksagonalnej z mechanikami RPG, takimi jak rozwój postaci, zarządzanie ekwipunkiem i zdobywanie coraz lepszego wyposażenia.

Istotnym założeniem jest stworzenie systemów, które będą ze sobą współpracować i pozwolą na dalsze rozwijanie projektu bez konieczności przebudowy całej architektury.
