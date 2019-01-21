using System.Collections.Generic;

namespace Posnet
{
    internal class PosnetException : FiscalException
    {
        private static Dictionary<int, PosnetExceptionItem> messages = new Dictionary<int, PosnetExceptionItem>();

        static PosnetException()
        {
            messages.Add(1, new PosnetExceptionItem(1, "PROTO_ERR_CMD_UNKNOWN", "Nierozpoznana komenda"));
            messages.Add(2, new PosnetExceptionItem(2, "PROTO_ERR_CMD_MANDATORY_FIELDS", "Brak obowiązkowego pola"));
            messages.Add(3, new PosnetExceptionItem(3, "PROTO_ERR_DATA_CONVERSION", "Błąd konwersji pola "));
            messages.Add(4, new PosnetExceptionItem(4, "PROTO_ERR_TOKEN_INVALID", "Błędny token"));
            messages.Add(5, new PosnetExceptionItem(5, "PROTO_ERR_CRC_INVALID", "Zła suma kontrolna"));
            messages.Add(6, new PosnetExceptionItem(6, "PROTO_ERR_FLD_INVALID", "Puste pole (kolejno dwa tabulatory)"));
            messages.Add(7, new PosnetExceptionItem(7, "PROTO_ERR_CMD_LENGTH", "Niewłaściwa długość nazwy rozkazu"));
            messages.Add(8, new PosnetExceptionItem(8, "PROTO_ERR_TOKEN_LENGTH", "Niewłaściwa długość tokena"));
            messages.Add(9, new PosnetExceptionItem(9, "PROTO_ERR_CRC_LENGTH", "Niewłaściwa długość sumy kontrolnej"));
            messages.Add(10, new PosnetExceptionItem(10, "PROTO_ERR_DATA_LENGTH", "Niewłaściwa długość pola danych"));
            messages.Add(11, new PosnetExceptionItem(11, "PROTO_ERR_INPUT_BUFFER_OVERRUN", "Zapełniony bufor odbiorczy"));
            messages.Add(12, new PosnetExceptionItem(12, "PROTO_ERR_CMD_IMMEDIATE_FORBIDDEN", "Nie można wykonać rozkazu w trybie natychmiastowym"));
            messages.Add(13, new PosnetExceptionItem(13, "PROTO_ERR_TOKEN_NOT_FOUND", "Nie znaleziono rozkazu o podanym tokenie"));
            messages.Add(14, new PosnetExceptionItem(14, "PROTO_ERR_INPUT_QUEUE_FULL", "Zapełniona kolejka wejściowa"));
            messages.Add(15, new PosnetExceptionItem(15, "PROTO_ERR_SYNTAX", "Błąd budowy ramki"));
            messages.Add(50, new PosnetExceptionItem(50, "ERR_UNKN", "Błąd wykonywania operacji przez kasę."));
            messages.Add(51, new PosnetExceptionItem(51, "ERR_ASSERT_FM", "Błąd wykonywania operacji przez kasę."));
            messages.Add(52, new PosnetExceptionItem(52, "ERR_ASSERT_DB", "Błąd wykonywania operacji przez kasę."));
            messages.Add(53, new PosnetExceptionItem(53, "ERR_ASSERT_SALE", "Błąd wykonywania operacji przez kasę."));
            messages.Add(54, new PosnetExceptionItem(54, "ERR_ASSERT_UI", "Błąd wykonywania operacji przez kasę."));
            messages.Add(55, new PosnetExceptionItem(55, "ERR_ASSERT_CFG", "Błąd wykonywania operacji przez kasę."));
            messages.Add(56, new PosnetExceptionItem(56, "ERR_ASSERT_CM", "Błąd wykonywania operacji przez kasę."));
            messages.Add(323, new PosnetExceptionItem(323, "ERR_OPER_BLOCKED", "Funkcja zablokowana w konfiguracji"));
            messages.Add(360, new PosnetExceptionItem(360, "ERR_SERVICE_SWITCH_FOUND", "znaleziono zworę serwisową"));
            messages.Add(361, new PosnetExceptionItem(361, "ERR_SERVICE_SWITCH_NOT_FOUND", "nie znaleziono zwory"));
            messages.Add(362, new PosnetExceptionItem(362, "ERR_SERVICE_KEY_DATA", "błąd weryfikacji danych klucza"));
            messages.Add(363, new PosnetExceptionItem(363, "ERR_SERVICE_KEY_TIMEOUT", "upłynął czas na odpowiedź od klucza"));
            messages.Add(382, new PosnetExceptionItem(382, "ERR_RD_ZERO", "próba wykonania raportu zerowego"));
            messages.Add(383, new PosnetExceptionItem(383, "ERR_RD_NOT_PRINTED", "Brak raportu dobowego."));
            messages.Add(384, new PosnetExceptionItem(384, "ERR_FM_NO_REC", "Brak rekordu w pamięci."));
            messages.Add(400, new PosnetExceptionItem(400, "ERR_WRONG_VALUE", "błędna wartość"));
            messages.Add(460, new PosnetExceptionItem(460, "ERR_CLOCK_RTC_FSK", "błąd zegara w trybie fiskalnym"));
            messages.Add(461, new PosnetExceptionItem(461, "ERR_CLOCK_RTC_NFSK", "błąd zegara w trybie niefiskalnym"));
            messages.Add(480, new PosnetExceptionItem(480, "ERR_AUTH_AUTHORIZED", "drukarka już autoryzowana bezterminowo"));
            messages.Add(481, new PosnetExceptionItem(481, "ERR_AUTH_NOT_STARTED", "nie rozpoczęto jeszcze autoryzacji"));
            messages.Add(482, new PosnetExceptionItem(482, "ERR_AUTH_WAS_ADDED", "kod już wprowadzony"));
            messages.Add(483, new PosnetExceptionItem(483, "ERR_AUTH_DAY_CNT", "próba wprowadzenia błędnych wartości"));
            messages.Add(484, new PosnetExceptionItem(484, "ERR_AUTH_BLOCKED", "Minął czas pracy kasy, sprzedaż zablokowana. Wprowadz kod autoryzacyjny drukarki."));
            messages.Add(485, new PosnetExceptionItem(485, "ERR_AUTH_BAD_CODE", "błędny kod autoryzacji"));
            messages.Add(486, new PosnetExceptionItem(486, "ERR_AUTH_TOO_MANY_WRONG_CODES", "Blokada autoryzacji. Wprowadź kod z klawiatury."));
            messages.Add(500, new PosnetExceptionItem(500, "ERR_STAT_MIN_OVF", "przepełnienie statystyki minimalnej"));
            messages.Add(501, new PosnetExceptionItem(501, "ERR_STAT_MAX_OVF", "przepełnienie statystyki maksymalnej"));
            messages.Add(502, new PosnetExceptionItem(502, "ERR_CASH_IN_MAX_OVF", "Przepełnienie stanu kasy"));
            messages.Add(503, new PosnetExceptionItem(503, "ERR_CASH_OUT_BELOW_0", "Wartość stanu kasy po wypłacie staje się ujemna (przyjmuje się stan zerowy kasy)"));
            messages.Add(700, new PosnetExceptionItem(700, "ERR_INVALID_IP_ADDR", "błędny adres IP"));
            messages.Add(701, new PosnetExceptionItem(701, "ERR_INVALID_TONE_NUMBER", "błąd numeru tonu"));
            messages.Add(702, new PosnetExceptionItem(702, "ERR_ILLEGAL_DRAWER_IMPULSE_LEN", "błąd długości impulsu szuflady"));
            messages.Add(703, new PosnetExceptionItem(703, "ERR_ILLEGAL_VAT_RATE", "błąd stawki VAT"));
            messages.Add(704, new PosnetExceptionItem(704, "ERR_INVALID_LOGOUT_TIME", "błąd czasu wylogowania"));
            messages.Add(705, new PosnetExceptionItem(705, "ERR_INVALID_SLEEP_TIME", "błąd czasu uśpienia"));
            messages.Add(706, new PosnetExceptionItem(706, "ERR_INVALID_TURNOFF_TIME", "błąd czasu wyłączenia"));
            messages.Add(713, new PosnetExceptionItem(713, "ERR_CONFIG_SET", "Błędne parametry konfiguracji"));
            messages.Add(714, new PosnetExceptionItem(714, "ERR_ILLEGAL_DSP_CONTRAST", "błędna wartość kontrastu wyświetlacza"));
            messages.Add(715, new PosnetExceptionItem(715, "ERR_ILLEGAL_DSP_LUMIN", "błędna wartość podświetlenia wyświetlacza"));
            messages.Add(716, new PosnetExceptionItem(716, "ERR_ILLEGAL_DSP_OFF_DELAY", "błędna wartość czasu zaniku podświetlenia"));
            messages.Add(717, new PosnetExceptionItem(717, "ERR_LINE_TOO_LONG", "za długa linia nagłówka albo stopki"));
            messages.Add(718, new PosnetExceptionItem(718, "ERR_ILLEGAL_COMM_CFG", "błędna konfiguracja komunikacji"));
            messages.Add(719, new PosnetExceptionItem(719, "ERR_ILLEGAL_PROTOCOL_CFG", "błędna konfiguracja protokołu kom."));
            messages.Add(720, new PosnetExceptionItem(720, "ERR_ILLEGAL_PORT", "błędny identyfikator portu"));
            messages.Add(721, new PosnetExceptionItem(721, "ERR_ILLEGAL_INFO_TXT_NUM", "błędny numer tekstu reklamowego"));
            messages.Add(722, new PosnetExceptionItem(722, "ERR_ILLEGAL_TIME_DIFF", "podany czas wychodzi poza wymagany zakres"));
            messages.Add(723, new PosnetExceptionItem(723, "ERR_ILLEGAL_TIME", "podana data/czas niepoprawne"));
            messages.Add(724, new PosnetExceptionItem(724, "ERR_ILLEGAL_HOUR_DIFF", "inna godzina w różnicach czasowych 0<=>23"));
            messages.Add(726, new PosnetExceptionItem(726, "ERR_ILLEGAL_DSP_LINE_CONTENT", "błędna zawartość tekstu w linii wyświetlacza"));
            messages.Add(727, new PosnetExceptionItem(727, "ERR_ILLEGAL_DSP_SCROLL_VALUE", "błędna wartość dla przewijania na wyświetlaczu"));
            messages.Add(728, new PosnetExceptionItem(728, "ERR_ILLEGAL_PORT_CFG", "błędna konfiguracja portu"));
            messages.Add(738, new PosnetExceptionItem(738, "ERR_ETH_CONFIG", "Nieprawidłowa konfiguracja Ethernetu"));
            messages.Add(739, new PosnetExceptionItem(739, "ERR_ILLEGAL_DSP_ID", "Nieprawidłowy typ wyświetlacza"));
            messages.Add(740, new PosnetExceptionItem(740, "ERR_ILLEGAL_DSP_ID_FOR_OFF_DELAY", "Dla tego typu wyświetlacza nie można ustawić czasu zaniku podświetlenia"));
            messages.Add(820, new PosnetExceptionItem(820, "ERR_TEST", "negatywny wynik testu"));
            messages.Add(821, new PosnetExceptionItem(821, "ERR_TEST_NO_CONF", "Brak testowanej opcji w konfiguracji"));
            messages.Add(857, new PosnetExceptionItem(857, "ERR_DF_DB_NO_MEM", "brak pamięci na inicjalizację bazy drukarkowej"));
            messages.Add(1000, new PosnetExceptionItem(1000, "ERR_FATAL_FM", "błąd fatalny modułu fiskalnego."));
            messages.Add(1001, new PosnetExceptionItem(1001, "ERR_FM_NCONN", "wypięta pamięć fiskalna"));
            messages.Add(1002, new PosnetExceptionItem(1002, "ERR_FM_WRITE", "błąd zapisu"));
            messages.Add(1003, new PosnetExceptionItem(1003, "ERR_FM_UNKN", "błąd nie ujęty w specyfikacji bios"));
            messages.Add(1004, new PosnetExceptionItem(1004, "ERR_FM_CHKSUM_CNT", "błędne sumy kontrolne"));
            messages.Add(1005, new PosnetExceptionItem(1005, "ERR_FM_CTRL_BLK_0", "błąd w pierwszym bloku kontrolnym"));
            messages.Add(1006, new PosnetExceptionItem(1006, "ERR_FM_CTRL_BLK_1", "błąd w drugim bloku kontrolnym"));
            messages.Add(1007, new PosnetExceptionItem(1007, "ERR_FM_BAD_REC_ID", "błędny id rekordu"));
            messages.Add(1008, new PosnetExceptionItem(1008, "ERR_FM_DATA_ADDR_INIT", "błąd inicjalizacji adresu startowego"));
            messages.Add(1009, new PosnetExceptionItem(1009, "ERR_FM_DATA_ADDR_INITED", "adres startowy zainicjalizowany"));
            messages.Add(1010, new PosnetExceptionItem(1010, "ERR_FM_NU_PRESENT", "numer unikatowy już zapisany"));
            messages.Add(1011, new PosnetExceptionItem(1011, "ERR_FM_NU_NO_PRESENT_FSK", "brak numeru w trybie fiskalnym"));
            messages.Add(1012, new PosnetExceptionItem(1012, "ERR_FM_NU_WRITE", "błąd zapisu numeru unikatowego"));
            messages.Add(1013, new PosnetExceptionItem(1013, "ERR_FM_NU_FULL", "przepełnienie numerów unikatowych"));
            messages.Add(1014, new PosnetExceptionItem(1014, "ERR_FM_NU_LANG", "błędny język w numerze unikatowym"));
            messages.Add(1015, new PosnetExceptionItem(1015, "ERR_FM_TIN_CNT", "więcej niż jeden NIP"));
            messages.Add(1016, new PosnetExceptionItem(1016, "ERR_FM_READ_ONLY_NFSK", "drukarka w trybie do odczytu bez rekordu fiskalizacji"));
            messages.Add(1017, new PosnetExceptionItem(1017, "ERR_FM_CLR_RAM_CNT", "przekroczono liczbę zerowań RAM"));
            messages.Add(1018, new PosnetExceptionItem(1018, "ERR_FM_REP_DAY_CNT", "przekroczono liczbę raportów dobowych"));
            messages.Add(1019, new PosnetExceptionItem(1019, "ERR_FM_VERIFY_NU", "błąd weryfikacji numeru unikatowego"));
            messages.Add(1020, new PosnetExceptionItem(1020, "ERR_FM_VERIFY_STAT", "błąd weryfikacji statystyk z RD."));
            messages.Add(1021, new PosnetExceptionItem(1021, "ERR_FM_VERIFY_NVR_READ", "błąd odczytu danych z NVR do weryfikacji FM"));
            messages.Add(1022, new PosnetExceptionItem(1022, "ERR_FM_VERIFY_NVR_WRITE", "błąd zapisu danych z NVR do weryfikacji FM"));
            messages.Add(1023, new PosnetExceptionItem(1023, "ERR_FM_CTRL_BLK_2", "pamięć fiskalna jest mała 1Mb zamiast 2Mb"));
            messages.Add(1024, new PosnetExceptionItem(1024, "ERR_FM_DATA_ADDR_NO_INITED", "nie zainicjalizowany obszar danych w pamięci fiskalnej"));
            messages.Add(1025, new PosnetExceptionItem(1025, "ERR_FM_NU_FORMAT", "błędny format numeru unikatowego"));
            messages.Add(1026, new PosnetExceptionItem(1026, "ERR_FM_REC_BAD_CNT", "za dużo błędnych bloków w FM"));
            messages.Add(1027, new PosnetExceptionItem(1027, "ERR_FM_NO_BADBLK_MARKER", "błąd oznaczenia błędnego bloku"));
            messages.Add(1028, new PosnetExceptionItem(1028, "ERR_FM_REC_EMPTY", "rekord w pamięci fiskalnej nie istnieje - obszar pusty"));
            messages.Add(1029, new PosnetExceptionItem(1029, "ERR_FM_REC_DATE", "rekord w pamięci fiskalnej z datą późniejszą od poprzedniego"));
            messages.Add(1950, new PosnetExceptionItem(1950, "ERR_TR_TOT_OVR", "przekroczony zakres totalizerów paragonu."));
            messages.Add(1951, new PosnetExceptionItem(1951, "ERR_TR_PF_OVR", "wpłata formą płatności przekracza max. wpłatę."));
            messages.Add(1952, new PosnetExceptionItem(1952, "ERR_TR_PF_SUM_OVR", "suma form płatności przekracza max. wpłatę."));
            messages.Add(1953, new PosnetExceptionItem(1953, "ERR_PAYMENT_OVR", "formy płatności pokrywają już do zapłaty."));
            messages.Add(1954, new PosnetExceptionItem(1954, "ERR_TR_CHANGE_OVR", "wpłata reszty przekracza max. wpłatę."));
            messages.Add(1955, new PosnetExceptionItem(1955, "ERR_TR_CHANGE_SUM_OVR", "suma form płatności przekracza max. wpłatę."));
            messages.Add(1956, new PosnetExceptionItem(1956, "ERR_TR_TOTAL_OVR", "przekroczony zakres total."));
            messages.Add(1957, new PosnetExceptionItem(1957, "ERR_TR_FISC_OVR", "przekroczony maksymalny zakres paragonu."));
            messages.Add(1958, new PosnetExceptionItem(1958, "ERR_TR_PACK_OVR", "przekroczony zakres wartości opakowań."));
            messages.Add(1959, new PosnetExceptionItem(1959, "ERR_TR_PACK_STORNO_OVR", "przekroczony zakres wartości opakowań przy stornowaniu."));
            messages.Add(1961, new PosnetExceptionItem(1961, "ERR_TR_PF_REST_TOO_BIG", "wpłata reszty zbyt duża"));
            messages.Add(1962, new PosnetExceptionItem(1962, "ERR_TR_PF_ZERO", "wpłata formą płatności wartości 0"));
            messages.Add(1980, new PosnetExceptionItem(1980, "ERR_TR_DISCNT_BASE_OVR", "przekroczony zakres kwoty bazowej rabatu/narzutu"));
            messages.Add(1981, new PosnetExceptionItem(1981, "ERR_TR_DISCNT_AFTER_OVR", "przekroczony zakres kwoty po rabacie / narzucie"));
            messages.Add(1982, new PosnetExceptionItem(1982, "ERR_TR_DISCNT_CALC", "błąd obliczania rabatu/narzutu"));
            messages.Add(1983, new PosnetExceptionItem(1983, "ERR_TR_DISCNT_BASE_NEGATIVE_OR_ZERO", "wartość bazowa ujemna lub równa 0"));
            messages.Add(1984, new PosnetExceptionItem(1984, "ERR_TR_DISCNT_ZERO", "wartość rabatu/narzutu zerowa"));
            messages.Add(1985, new PosnetExceptionItem(1985, "ERR_TR_DISCNT_AFTER_NEGATIVE_OR_ZERO", "wartość po rabacie ujemna lub równa 0"));
            messages.Add(1990, new PosnetExceptionItem(1990, "ERR_TR_STORNO_NOT_ALLOWED", "Niedozwolone stornowanie towaru. Błędny stan transakcji."));
            messages.Add(1991, new PosnetExceptionItem(1991, "ERR_TR_DISCNT_NOT_ALLOWED", "Niedozwolony rabat/narzut. Błędny stan transakcji."));
            messages.Add(2000, new PosnetExceptionItem(2000, "ERR_TR_FLD_VAT", "błąd pola VAT."));
            messages.Add(2002, new PosnetExceptionItem(2002, "ERR_NO_HDR", "brak nagłówka"));
            messages.Add(2003, new PosnetExceptionItem(2003, "ERR_HDR", "zaprogramowany nagłówek"));
            messages.Add(2004, new PosnetExceptionItem(2004, "ERR_NO_VAT", "brak aktywnych stawek VAT."));
            messages.Add(2005, new PosnetExceptionItem(2005, "ERR_NO_TRNS_MODE", "brak trybu transakcji."));
            messages.Add(2006, new PosnetExceptionItem(2006, "ERR_TR_FLD_PRICE", "błąd pola cena ( cena <= 0 )"));
            messages.Add(2007, new PosnetExceptionItem(2007, "ERR_TR_FLD_QUANT", "błąd pola ilość ( ilość <= 0 )"));
            messages.Add(2008, new PosnetExceptionItem(2008, "ERR_TR_FLD_TOTAL", "błąd kwoty total"));
            messages.Add(2009, new PosnetExceptionItem(2009, "ERR_TR_FLD_TOTAL_ZERO", "błąd kwoty total, równa zero"));
            messages.Add(2010, new PosnetExceptionItem(2010, "ERR_TOT_OVR", "przekroczony zakres totalizerów dobowych."));
            messages.Add(2021, new PosnetExceptionItem(2021, "ERR_RTC_WAS_SET", "próba ponownego ustawienia zegara."));
            messages.Add(2022, new PosnetExceptionItem(2022, "ERR_RTC_DIFF", "zbyt duża różnica dat"));
            messages.Add(2023, new PosnetExceptionItem(2023, "ERR_RTC_HOUR", "różnica większa niż godzina w trybie użytkownika w trybie fiskalnym."));
            messages.Add(2024, new PosnetExceptionItem(2024, "ERR_RTC_BAD_FORMAT", "zły format daty (np. 13 miesiąc )"));
            messages.Add(2025, new PosnetExceptionItem(2025, "ERR_RTC_FM_DATE", "data wcześniejsza od ostatniego zapisu do modułu"));
            messages.Add(2026, new PosnetExceptionItem(2026, "ERR_RTC", "błąd zegara."));
            messages.Add(2027, new PosnetExceptionItem(2027, "ERR_VAT_CHNG_CNT", "przekroczono maksymalną liczbę zmian stawek VAT"));
            messages.Add(2028, new PosnetExceptionItem(2028, "ERR_VAT_SAME", "próba zdefiniowana identycznych stawek VAT"));
            messages.Add(2029, new PosnetExceptionItem(2029, "ERR_VAT_VAL", "błędne wartości stawek VAT"));
            messages.Add(2030, new PosnetExceptionItem(2030, "ERR_VAT_NO_ACTIVE", "próba zdefiniowania stawek VAT wszystkich nieaktywnych"));
            messages.Add(2031, new PosnetExceptionItem(2031, "ERR_FLD_TIN", "błąd pola NIP."));
            messages.Add(2032, new PosnetExceptionItem(2032, "ERR_FM_ID", "błąd numeru unikatowego pamięci fiskalnej."));
            messages.Add(2033, new PosnetExceptionItem(2033, "ERR_FISC_MODE", "urządzenie w trybie fiskalnym."));
            messages.Add(2034, new PosnetExceptionItem(2034, "ERR_NO_FISC_MODE", "urządzenie w trybie niefiskalnym."));
            messages.Add(2035, new PosnetExceptionItem(2035, "ERR_TOT_NOT_ZERO", "niezerowe totalizery."));
            messages.Add(2036, new PosnetExceptionItem(2036, "ERR_READ_ONLY", "urządzenie w stanie tylko do odczytu."));
            messages.Add(2037, new PosnetExceptionItem(2037, "ERR_NO_READ_ONLY", "urządzenie nie jest w stanie tylko do odczytu."));
            messages.Add(2038, new PosnetExceptionItem(2038, "ERR_TRNS_MODE", "urządzenie w trybie transakcji."));
            messages.Add(2039, new PosnetExceptionItem(2039, "ERR_TOT_ZERO", "zerowe totalizery."));
            messages.Add(2040, new PosnetExceptionItem(2040, "ERR_CURR_CALC", "błąd obliczeń walut, przepełnienie przy mnożeniu lub dzieleniu."));
            messages.Add(2041, new PosnetExceptionItem(2041, "ERR_TR_END_VAL_0", "próba zakończenia pozytywnego paragonu z wartością 0"));
            messages.Add(2042, new PosnetExceptionItem(2042, "ERR_REP_PER_DATE_FORMAT_FROM", "błędy format daty początkowej"));
            messages.Add(2043, new PosnetExceptionItem(2043, "ERR_REP_PER_DATE_FORMAT_TO", "błędy format daty końcowej"));
            messages.Add(2044, new PosnetExceptionItem(2044, "ERR_REP_PER_CURR_MONTH", "próba wykonania raportu miesięcznego w danym miesiącu"));
            messages.Add(2045, new PosnetExceptionItem(2045, "ERR_REP_PER_DATE_START_GT_CURR", "data początkowa późniejsza od bieżącej daty"));
            messages.Add(2046, new PosnetExceptionItem(2046, "ERR_REP_PER_DATE_END_GT_FISK", "data końcowa wcześniejsza od daty fiskalizacji"));
            messages.Add(2047, new PosnetExceptionItem(2047, "ERR_REP_PER_NUM_ZERO", "numer początkowy lub końcowy równy zero"));
            messages.Add(2048, new PosnetExceptionItem(2048, "ERR_REP_PER_NUM_FROM_GT_END", "numer początkowy większy od numeru końcowego"));
            messages.Add(2049, new PosnetExceptionItem(2049, "ERR_REP_PER_NUM_TOO_BIG", "numer raportu zbyt duży"));
            messages.Add(2050, new PosnetExceptionItem(2050, "ERR_REP_PER_DATE_END_GT_START", "data początkowa późniejsza od daty końcowej"));
            messages.Add(2051, new PosnetExceptionItem(2051, "ERR_REP_TXT_NO_MEM", "brak pamięci w buforze tekstów."));
            messages.Add(2052, new PosnetExceptionItem(2052, "ERR_TR_NO_MEM", "brak pamięci w buforze transakcji"));
            messages.Add(2054, new PosnetExceptionItem(2054, "ERR_TR_END_PAYMENT", "formy płatności nie pokrywają kwoty do zapłaty lub reszty"));
            messages.Add(2055, new PosnetExceptionItem(2055, "ERR_LINE", "błędna linia"));
            messages.Add(2056, new PosnetExceptionItem(2056, "ERR_EMPTY_TXT", "tekst pusty"));
            messages.Add(2057, new PosnetExceptionItem(2057, "ERR_SIZE", "przekroczony rozmiar"));
            messages.Add(2058, new PosnetExceptionItem(2058, "ERR_LINE_CNT", "błędna liczba linii."));
            messages.Add(2060, new PosnetExceptionItem(2060, "ERR_TR_BAD_STATE", "błędny stan transakcji"));
            messages.Add(2062, new PosnetExceptionItem(2062, "ERR_PRN_START", "jest wydrukowana część jakiegoś dokumentu"));
            messages.Add(2063, new PosnetExceptionItem(2063, "ERR_PAR", "błąd parametru"));
            messages.Add(2064, new PosnetExceptionItem(2064, "ERR_FTR_NO_HDR", "brak rozpoczęcia wydruku lub transakcji"));
            messages.Add(2067, new PosnetExceptionItem(2067, "ERR_PRN_CFG_SET", "błąd ustawień konfiguracyjnych wydruków / drukarki"));
            messages.Add(2070, new PosnetExceptionItem(2070, "ERR_WRONG_MAINTENANCE_TIME", "Data przeglądu wcześniejsza od systemowej"));
            messages.Add(2101, new PosnetExceptionItem(2101, "ERR_DF_DB_OVR", "Zapełnienie bazy"));
            messages.Add(2102, new PosnetExceptionItem(2102, "ERR_DF_DB_VAT_INACTIVE", "Stawka nieaktywna"));
            messages.Add(2103, new PosnetExceptionItem(2103, "ERR_DF_DB_VAT_INVALID", "Nieprawidłowa stawka VAT"));
            messages.Add(2104, new PosnetExceptionItem(2104, "ERR_DF_DB_NAME", "Błąd nazwy"));
            messages.Add(2105, new PosnetExceptionItem(2105, "ERR_DF_DB_NAME_VAT", "Błąd przypisania stawki"));
            messages.Add(2106, new PosnetExceptionItem(2106, "ERR_DF_DB_LOCKED", "Towar został zablokowany przez drukarkę"));
            messages.Add(2107, new PosnetExceptionItem(2107, "ERR_DF_DB_NAME_NOT_FOUND", "Nie znaleziono w bazie drukarkowej"));
            messages.Add(2108, new PosnetExceptionItem(2108, "ERR_DF_DB_NOT_OVR", "baza nie jest zapełniona"));
            messages.Add(2501, new PosnetExceptionItem(2501, "ERR_FORM_ID", "Błędny identyfikator raportu"));
            messages.Add(2502, new PosnetExceptionItem(2502, "ERR_FORM_LINE_NO", "Błędny identyfikator linii raportu"));
            messages.Add(2503, new PosnetExceptionItem(2503, "ERR_FORM_HDR_NO", "Błędny identyfikator nagłówka raportu"));
            messages.Add(2504, new PosnetExceptionItem(2504, "ERR_FORM_PAR_CNT", "Zbyt mało parametrów raportu"));
            messages.Add(2505, new PosnetExceptionItem(2505, "ERR_FORM_NOT_STARTED", "Raport nie rozpoczęty"));
            messages.Add(2506, new PosnetExceptionItem(2506, "ERR_FORM_STARTED", "Raport rozpoczęty"));
            messages.Add(2507, new PosnetExceptionItem(2507, "ERR_FORM_CMD", "Błędny identyfikator komendy"));
            messages.Add(2521, new PosnetExceptionItem(2521, "ERR_DB_REP_PLU_ACTIVE", "Raport już rozpoczęty"));
            messages.Add(2522, new PosnetExceptionItem(2522, "ERR_DB_REP_PLU_NOT_ACTIVE", "Raport nie rozpoczęty"));
            messages.Add(2523, new PosnetExceptionItem(2523, "ERR_DB_REP_PLU_VAT_ID", "Błędna stawka VAT"));
            messages.Add(2532, new PosnetExceptionItem(2532, "ERR_FV_COPY_CNT", "Błędna liczba kopii faktur"));
            messages.Add(2600, new PosnetExceptionItem(2600, "ERR_DISCNT_TYPE", "Błędny typ rabatu/narzutu"));
            messages.Add(2601, new PosnetExceptionItem(2601, "ERR_TR_DISCNT_VALUE", "wartość rabatu/narzutu spoza zakresu"));
            messages.Add(2701, new PosnetExceptionItem(2701, "ERR_VAT_ID", "Błąd identyfikatora stawki podatkowej."));
            messages.Add(2702, new PosnetExceptionItem(2702, "ERR_FTRLN_ID", "Błędny identyfikator dodatkowej stopki."));
            messages.Add(2703, new PosnetExceptionItem(2703, "ERR_FTRLN_CNT", "Przekroczona liczba dodatkowych stopek."));
            messages.Add(2704, new PosnetExceptionItem(2704, "ERR_ACC_LOW", "Zbyt słaby akumulator."));
            messages.Add(2705, new PosnetExceptionItem(2705, "ERR_PF_TYPE", "Błędny identyfikator typu formy płatności."));
            messages.Add(2801, new PosnetExceptionItem(2801, "ERR_DISCNT_VERIFY", "Błąd weryfikacji wartości rabatu/narzutu"));
            messages.Add(2802, new PosnetExceptionItem(2802, "ERR_LNTOT_VERIFY", "Błąd weryfikacji wartości linii sprzedaży"));
            messages.Add(2803, new PosnetExceptionItem(2803, "ERR_PACKTOT_VERIFY", "Błąd weryfikacji wartości opakowania"));
            messages.Add(2804, new PosnetExceptionItem(2804, "ERR_CURRTOT_VERIFY", "Błąd weryfikacji wartości formy płatności"));
            messages.Add(2805, new PosnetExceptionItem(2805, "ERR_ENDTOT_VERIFY", "Błąd weryfikacji wartości fiskalnej"));
            messages.Add(2806, new PosnetExceptionItem(2806, "ERR_ENDPACKPLUS_VERIFY", "Błąd weryfikacji wartości opakowań dodatnich"));
            messages.Add(2807, new PosnetExceptionItem(2807, "ERR_ENDPACKMINUS_VERIFY", "Błąd weryfikacji wartości opakowań ujemnych"));
            messages.Add(2808, new PosnetExceptionItem(2808, "ERR_ENDPAYMENT_VERIFY", "Błąd weryfikacji wartości wpłaconych form płatności"));
            messages.Add(2809, new PosnetExceptionItem(2809, "ERR_ENDCHANGE_VERIFY", "Błąd weryfikacji wartości reszt"));
            messages.Add(2851, new PosnetExceptionItem(2851, "ERR_STORNO_QNT", "Błąd stornowania, błędna ilość"));
            messages.Add(2852, new PosnetExceptionItem(2852, "ERR_STORNO_AMT", "Błąd stornowania, błędna wartość"));
            messages.Add(2903, new PosnetExceptionItem(2903, "ERR", "Pamięć podręczna kopii elektronicznej zawiera zbyt dużą ilość danych"));
        }

        public PosnetException(string msg) : base(msg)
        {
            message = msg;
            type = 0;
        }

        public PosnetException(int num, string msg, ExceptionType excType) : base(msg)
        {
            number = num;
            message = msg;
            type = excType;
        }

        public PosnetException(int num) : base("Drukarka zwrócila nieznany błąd: " + num)
        {
            if (messages.ContainsKey(num))
                message = (messages[num].Mnemonik + " " + messages[num].Description);
            number = num;
            type = 0;
        }

        public PosnetException(int num, string currentCommand) : base("Drukarka zwrócila nieznany błąd: " + num + " podczas wykonywania polecenia:" + currentCommand)
        {
            if (messages.ContainsKey(num))
                message = ("Błąd podczas wykonywania polecenia " + currentCommand + ": " + messages[num].Mnemonik + " " + messages[num].Description);
            number = num;
            type = 0;
        }
    }
}
