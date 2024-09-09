# SimplePoll

### Konfiguracja uruchomienia w VS Studio

Należy ustawić uruchomienie naraz dwóch projektów: SimplePoll.CoreServer oraz SimplePoll.WebApi (prawy na solucje w Solution Explorer, właściwości i wybrać Multiple startup projects)

Następnie Rebuild Solution i można uruchamiać. Należy zwrócić uwagę by odpalał się tryb http bo z takimi portami skonfigurowane są dostępy (SimplePoll.WebApi i SimplePoll.WebClient)

### Konfiguracja bazy

Należy założyć nową pustą bazę i wypełnić wartość SimplePoll.CoreServer/appsettings.json -> ConnectionStrings/SimplePollDB właciwym dostępem, użytkownik jako db_owner

Następnie należy zainstalować sobie narzędzie `dotnet-ef` jeśli jeszcze go nie mamy poleceniem

`dotnet tool install --global dotnet-ef --version 8.*`

Po tym można uruchomić

`dotnet ef database update`

By migracje utworzyły wymagane tabele oraz wypełniły testowymi danymi (jest jeden formularz skonfigurowany według wytycznych zadania)

### Konfiguracja uruchomienia projektu Angular

W folderze projektu SimplePoll.WebClient:

`npm install` 

`npm run start`

Otworzyć przeglądarke `http://localhost:4200`

# Architektura rozwiązania

Mamy 3 głowne projekty:
* `SimplePoll.CoreServer` - właściwy projekt odpowiadający za komunikację z bazą danych
* `SimplePoll.WebApi` - projekt proxy API dla klienta przeglądarkowego, tak by nie wywoływać bezpośrednio metod projektu głownego
* `SimplePoll.WebClient` - aplikacja SPA napisana w Angular która zajmuje się wyświetleniem formularza, oraz wysłaniem odpowiedzi

Pomiędzy `SimplePoll.CoreServer` a `SimplePoll.WebApi` jest zrobiona bardzo prosta weryfikacja tokenu który może być dowolnym ciągiem znaków, jeśli miałbym spędzić na tym więcej czasu, zrobił bym autoryzację certyfikatem klienckim ze sprawdzeniem odcisku certyfikatu oraz jego walidacją. Ale na potrzeby zadania wystarczy taka autoryzacja (swagger jest z niej wyłączony).

`SimplePoll.WebClient` odwołuje się bezpośrednio do `SimplePoll.WebApi` korzystając wyłącznie ze specyfikacji Dto, które są pisane ręcznie. Przy większej ilości czasu pokusiłbym się o generator typów dla Typescript choć by w T4 przy budowaniu projektu `SimplePoll.WebApi`.

`SimplePoll.WebApi` odwołuje się już do metod `SimplePoll.CoreServer` ale równiez korzystając jedynie z Dto które znajdują się w projkecie współnym `SimplePoll.Common`.

Zamianą Dto na faktyczne modele bazodanowe zajmuje się już Autommaper w projekcie serwera głównego.

Projekt nie zawiera możliwości wyświetlania rezultatów ale dodanie takiej funkcjonalności było by bardzo proste. Wszystkie zgłoszone odpowiedzi do ankiet zapisywane są w tabelach `Polls.Submissions` i powiązanych `Polls.Answers` w formie ciągu znaków (w przypadku złożonych odpowiedzi wielokrotnego wyboru są to serializowane tablice w formacie JSON). Parsowanie tych danych wymaga poznania definicji pytań ale to jest załączone do konkretnych odpowiedzi także bez problemu można to wykonać.

# Brakujące rzeczy

Projekt Angular nie posiada żadnej funkcjonalności tłumaczeń, także szablonach oraz w paru miejscach w kodzie ciągi są napisane w języku polskim, co oczywiście należało by zmienić.

Żaden z projektów nie posiada testów jednostkowych i jest to rzecz którą jako pierwszą należałoby uzupełnić, lecz przy ilości wygenerowanego kodu niestety zabrakło czasu.

Projekt w pierwszym założeniu miał opierać się na uruchamianaych kontenerach Docker ale zrezygnowałem z dodatkowej pracy, gdyż uznałem że na tym etapie będzie to zbędne.

### Kwestia autentykacji i autoryzacji

Rozdzielenie na `SimplePoll.CoreServer` i `SimplePoll.WebApi` pozwala na zdefiniowanie całego procesu autentykacji i autoryzacji dwutorowo. Przy założeniu że `SimplePoll.WebApi` odwoływałby się do `SimplePoll.CoreServer` posługując się certyfikatami, można by pokusić się na pozostawienie tego jako jedynej formy autentykacji oraz autoryzacji (autoryzację można by rozwinąć bardziej ale wiążąc z odciskiem certyfikatu) w komunikacji `serwer<->serwer`

W przypadku autentykacji dla `SimplePoll.WebClient` można skorzystać z kilku rozwiązań:
* Możemy skorzystać z możliwości postawienia serwera KeyCloak i używać go jako dostawcy logowania typu OpenId
* Możemy skorzystać z rozwiązań Microsoft np IdentityServer który służyłby do tej samej funkcjonalności
* Jeśli aplikacja działa w środowisku LDAP można by skorzystać z logowania w ramach katalogu LDAP i autentykować użytkowników
* Autoryzacja wymagałaby jednak zdefiniowania roli i dotępu konkretnych użytkowników już we własnej bazie

Wszystkie powyższe rozwiązania zakładają że autentykacją i autoryzacją dla aplikacji SPA zajmie się jedynie `SimplePoll.WebApi` gdyż tylko w tym zakresie wymagana jest walidacja użytkownika końcowego, `SimplePoll.CoreServer` nie ma i raczej nie musi miec o nim pojęcia, co uprości możliwą logikę biznesową zarządzania formularzami.

W uproszczeniu `SimplePoll.WebClient` za pomocą HttpInterceptora wykrywałby żądania które zwracają 401 (token wygasł) bądź w przypadku braku tokenu autoryzacyjnego, przekierowują użytkownika do procesu logowania i zwrócony nowy jwt token zapisują sobie lokalnie oraz dołączają do każdego ponownego żądania.

Można też token zapisać w bezpiecznym ciasteczku i w ogóle nie przejmować się jego przechowywaniem po stronie aplikacji SPA.

### Co można zrobić jeszcze

Przy większej ilości czasu trzeba by przede wszystkim dodać logowanie błędów aplikacji, w tym momencie żaden z projektów nie instancjonuje nigdzie ILogger<T> i nie wrzuca tam błędów aplikacyjnych, logowanie błędów odbywa się na zasadzie wyrzucenia wyjątku do konsoli, co można przechwycić ale jest trudne w automatycznej interpretacji (lepszy jest ustandaryzowany format błędów i ich obsługa), dlatego jeśli chcemy przerzucać błędy dalej do np. Splunka czy AppInsights to należy nieco ten format poprawić.

Moduł parsujący konfigurację ankiety należało by rozbić na małe komponenty odpowiadające konkretnym typom pól, w tym momencie jest to jedna pętla i garstka wyrażeń warunkowych