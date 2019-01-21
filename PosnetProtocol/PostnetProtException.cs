// Decompiled with JetBrains decompiler
// Type: OnlineFPPosnetProtocol.PostnetProtException
// Assembly: OnlineFPPosnetProtocol, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 619FDB08-8046-432B-AE81-BF4303AA2094
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\drivers\OnlineFPPosnetProtocol.dll

using OnlineFPCommon;
using System.Collections.Generic;

namespace OnlineFPPosnetProtocol
{
  internal class PostnetProtException : OFPException
  {
    private static Dictionary<int, PostnetProtExceptionItem> messages = new Dictionary<int, PostnetProtExceptionItem>();

    static PostnetProtException()
    {
      PostnetProtException.messages.Add(1, new PostnetProtExceptionItem(1, "PROTO_ERR_CMD_UNKNOWN", "Nierozpoznana komenda"));
      PostnetProtException.messages.Add(2, new PostnetProtExceptionItem(2, "PROTO_ERR_CMD_MANDATORY_FIELDS", "Brak obowiązkowego pola"));
      PostnetProtException.messages.Add(3, new PostnetProtExceptionItem(3, "PROTO_ERR_DATA_CONVERSION", "Błąd konwersji pola "));
      PostnetProtException.messages.Add(4, new PostnetProtExceptionItem(4, "PROTO_ERR_TOKEN_INVALID", "Błędny token"));
      PostnetProtException.messages.Add(5, new PostnetProtExceptionItem(5, "PROTO_ERR_CRC_INVALID", "Zła suma kontrolna"));
      PostnetProtException.messages.Add(6, new PostnetProtExceptionItem(6, "PROTO_ERR_FLD_INVALID", "Puste pole (kolejno dwa tabulatory)"));
      PostnetProtException.messages.Add(7, new PostnetProtExceptionItem(7, "PROTO_ERR_CMD_LENGTH", "Niewłaściwa długość nazwy rozkazu"));
      PostnetProtException.messages.Add(8, new PostnetProtExceptionItem(8, "PROTO_ERR_TOKEN_LENGTH", "Niewłaściwa długość tokena"));
      PostnetProtException.messages.Add(9, new PostnetProtExceptionItem(9, "PROTO_ERR_CRC_LENGTH", "Niewłaściwa długość sumy kontrolnej"));
      PostnetProtException.messages.Add(10, new PostnetProtExceptionItem(10, "PROTO_ERR_DATA_LENGTH", "Niewłaściwa długość pola danych"));
      PostnetProtException.messages.Add(11, new PostnetProtExceptionItem(11, "PROTO_ERR_INPUT_BUFFER_OVERRUN", "Zapełniony bufor odbiorczy"));
      PostnetProtException.messages.Add(12, new PostnetProtExceptionItem(12, "PROTO_ERR_CMD_IMMEDIATE_FORBIDDEN", "Nie można wykonać rozkazu w trybie natychmiastowym"));
      PostnetProtException.messages.Add(13, new PostnetProtExceptionItem(13, "PROTO_ERR_TOKEN_NOT_FOUND", "Nie znaleziono rozkazu o podanym tokenie"));
      PostnetProtException.messages.Add(14, new PostnetProtExceptionItem(14, "PROTO_ERR_INPUT_QUEUE_FULL", "Zapełniona kolejka wejściowa"));
      PostnetProtException.messages.Add(15, new PostnetProtExceptionItem(15, "PROTO_ERR_SYNTAX", "Błąd budowy ramki"));
      PostnetProtException.messages.Add(50, new PostnetProtExceptionItem(50, "ERR_UNKN", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(51, new PostnetProtExceptionItem(51, "ERR_ASSERT_FM", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(52, new PostnetProtExceptionItem(52, "ERR_ASSERT_DB", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(53, new PostnetProtExceptionItem(53, "ERR_ASSERT_SALE", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(54, new PostnetProtExceptionItem(54, "ERR_ASSERT_UI", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(55, new PostnetProtExceptionItem(55, "ERR_ASSERT_CFG", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(56, new PostnetProtExceptionItem(56, "ERR_ASSERT_CM", "Błąd wykonywania operacji przez kasę."));
      PostnetProtException.messages.Add(323, new PostnetProtExceptionItem(323, "ERR_OPER_BLOCKED", "Funkcja zablokowana w konfiguracji"));
      PostnetProtException.messages.Add(360, new PostnetProtExceptionItem(360, "ERR_SERVICE_SWITCH_FOUND", "znaleziono zworę serwisową"));
      PostnetProtException.messages.Add(361, new PostnetProtExceptionItem(361, "ERR_SERVICE_SWITCH_NOT_FOUND", "nie znaleziono zwory"));
      PostnetProtException.messages.Add(362, new PostnetProtExceptionItem(362, "ERR_SERVICE_KEY_DATA", "błąd weryfikacji danych klucza"));
      PostnetProtException.messages.Add(363, new PostnetProtExceptionItem(363, "ERR_SERVICE_KEY_TIMEOUT", "upłynął czas na odpowiedź od klucza"));
      PostnetProtException.messages.Add(382, new PostnetProtExceptionItem(382, "ERR_RD_ZERO", "próba wykonania raportu zerowego"));
      PostnetProtException.messages.Add(383, new PostnetProtExceptionItem(383, "ERR_RD_NOT_PRINTED", "Brak raportu dobowego."));
      PostnetProtException.messages.Add(384, new PostnetProtExceptionItem(384, "ERR_FM_NO_REC", "Brak rekordu w pamięci."));
      PostnetProtException.messages.Add(400, new PostnetProtExceptionItem(400, "ERR_WRONG_VALUE", "błędna wartość"));
      PostnetProtException.messages.Add(460, new PostnetProtExceptionItem(460, "ERR_CLOCK_RTC_FSK", "błąd zegara w trybie fiskalnym"));
      PostnetProtException.messages.Add(461, new PostnetProtExceptionItem(461, "ERR_CLOCK_RTC_NFSK", "błąd zegara w trybie niefiskalnym"));
      PostnetProtException.messages.Add(480, new PostnetProtExceptionItem(480, "ERR_AUTH_AUTHORIZED", "drukarka już autoryzowana bezterminowo"));
      PostnetProtException.messages.Add(481, new PostnetProtExceptionItem(481, "ERR_AUTH_NOT_STARTED", "nie rozpoczęto jeszcze autoryzacji"));
      PostnetProtException.messages.Add(482, new PostnetProtExceptionItem(482, "ERR_AUTH_WAS_ADDED", "kod już wprowadzony"));
      PostnetProtException.messages.Add(483, new PostnetProtExceptionItem(483, "ERR_AUTH_DAY_CNT", "próba wprowadzenia błędnych wartości"));
      PostnetProtException.messages.Add(484, new PostnetProtExceptionItem(484, "ERR_AUTH_BLOCKED", "Minął czas pracy kasy, sprzedaż zablokowana. Wprowadz kod autoryzacyjny drukarki."));
      PostnetProtException.messages.Add(485, new PostnetProtExceptionItem(485, "ERR_AUTH_BAD_CODE", "błędny kod autoryzacji"));
      PostnetProtException.messages.Add(486, new PostnetProtExceptionItem(486, "ERR_AUTH_TOO_MANY_WRONG_CODES", "Blokada autoryzacji. Wprowadź kod z klawiatury."));
      PostnetProtException.messages.Add(500, new PostnetProtExceptionItem(500, "ERR_STAT_MIN_OVF", "przepełnienie statystyki minimalnej"));
      PostnetProtException.messages.Add(501, new PostnetProtExceptionItem(501, "ERR_STAT_MAX_OVF", "przepełnienie statystyki maksymalnej"));
      PostnetProtException.messages.Add(502, new PostnetProtExceptionItem(502, "ERR_CASH_IN_MAX_OVF", "Przepełnienie stanu kasy"));
      PostnetProtException.messages.Add(503, new PostnetProtExceptionItem(503, "ERR_CASH_OUT_BELOW_0", "Wartość stanu kasy po wypłacie staje się ujemna (przyjmuje się stan zerowy kasy)"));
      PostnetProtException.messages.Add(700, new PostnetProtExceptionItem(700, "ERR_INVALID_IP_ADDR", "błędny adres IP"));
      PostnetProtException.messages.Add(701, new PostnetProtExceptionItem(701, "ERR_INVALID_TONE_NUMBER", "błąd numeru tonu"));
      PostnetProtException.messages.Add(702, new PostnetProtExceptionItem(702, "ERR_ILLEGAL_DRAWER_IMPULSE_LEN", "błąd długości impulsu szuflady"));
      PostnetProtException.messages.Add(703, new PostnetProtExceptionItem(703, "ERR_ILLEGAL_VAT_RATE", "błąd stawki VAT"));
      PostnetProtException.messages.Add(704, new PostnetProtExceptionItem(704, "ERR_INVALID_LOGOUT_TIME", "błąd czasu wylogowania"));
      PostnetProtException.messages.Add(705, new PostnetProtExceptionItem(705, "ERR_INVALID_SLEEP_TIME", "błąd czasu uśpienia"));
      PostnetProtException.messages.Add(706, new PostnetProtExceptionItem(706, "ERR_INVALID_TURNOFF_TIME", "błąd czasu wyłączenia"));
      PostnetProtException.messages.Add(713, new PostnetProtExceptionItem(713, "ERR_CONFIG_SET", "Błędne parametry konfiguracji"));
      PostnetProtException.messages.Add(714, new PostnetProtExceptionItem(714, "ERR_ILLEGAL_DSP_CONTRAST", "błędna wartość kontrastu wyświetlacza"));
      PostnetProtException.messages.Add(715, new PostnetProtExceptionItem(715, "ERR_ILLEGAL_DSP_LUMIN", "błędna wartość podświetlenia wyświetlacza"));
      PostnetProtException.messages.Add(716, new PostnetProtExceptionItem(716, "ERR_ILLEGAL_DSP_OFF_DELAY", "błędna wartość czasu zaniku podświetlenia"));
      PostnetProtException.messages.Add(717, new PostnetProtExceptionItem(717, "ERR_LINE_TOO_LONG", "za długa linia nagłówka albo stopki"));
      PostnetProtException.messages.Add(718, new PostnetProtExceptionItem(718, "ERR_ILLEGAL_COMM_CFG", "błędna konfiguracja komunikacji"));
      PostnetProtException.messages.Add(719, new PostnetProtExceptionItem(719, "ERR_ILLEGAL_PROTOCOL_CFG", "błędna konfiguracja protokołu kom."));
      PostnetProtException.messages.Add(720, new PostnetProtExceptionItem(720, "ERR_ILLEGAL_PORT", "błędny identyfikator portu"));
      PostnetProtException.messages.Add(721, new PostnetProtExceptionItem(721, "ERR_ILLEGAL_INFO_TXT_NUM", "błędny numer tekstu reklamowego"));
      PostnetProtException.messages.Add(722, new PostnetProtExceptionItem(722, "ERR_ILLEGAL_TIME_DIFF", "podany czas wychodzi poza wymagany zakres"));
      PostnetProtException.messages.Add(723, new PostnetProtExceptionItem(723, "ERR_ILLEGAL_TIME", "podana data/czas niepoprawne"));
      PostnetProtException.messages.Add(724, new PostnetProtExceptionItem(724, "ERR_ILLEGAL_HOUR_DIFF", "inna godzina w różnicach czasowych 0<=>23"));
      PostnetProtException.messages.Add(726, new PostnetProtExceptionItem(726, "ERR_ILLEGAL_DSP_LINE_CONTENT", "błędna zawartość tekstu w linii wyświetlacza"));
      PostnetProtException.messages.Add(727, new PostnetProtExceptionItem(727, "ERR_ILLEGAL_DSP_SCROLL_VALUE", "błędna wartość dla przewijania na wyświetlaczu"));
      PostnetProtException.messages.Add(728, new PostnetProtExceptionItem(728, "ERR_ILLEGAL_PORT_CFG", "błędna konfiguracja portu"));
      PostnetProtException.messages.Add(738, new PostnetProtExceptionItem(738, "ERR_ETH_CONFIG", "Nieprawidłowa konfiguracja Ethernetu"));
      PostnetProtException.messages.Add(739, new PostnetProtExceptionItem(739, "ERR_ILLEGAL_DSP_ID", "Nieprawidłowy typ wyświetlacza"));
      PostnetProtException.messages.Add(740, new PostnetProtExceptionItem(740, "ERR_ILLEGAL_DSP_ID_FOR_OFF_DELAY", "Dla tego typu wyświetlacza nie można ustawić czasu zaniku podświetlenia"));
      PostnetProtException.messages.Add(820, new PostnetProtExceptionItem(820, "ERR_TEST", "negatywny wynik testu"));
      PostnetProtException.messages.Add(821, new PostnetProtExceptionItem(821, "ERR_TEST_NO_CONF", "Brak testowanej opcji w konfiguracji"));
      PostnetProtException.messages.Add(857, new PostnetProtExceptionItem(857, "ERR_DF_DB_NO_MEM", "brak pamięci na inicjalizację bazy drukarkowej"));
      PostnetProtException.messages.Add(1000, new PostnetProtExceptionItem(1000, "ERR_FATAL_FM", "błąd fatalny modułu fiskalnego."));
      PostnetProtException.messages.Add(1001, new PostnetProtExceptionItem(1001, "ERR_FM_NCONN", "wypięta pamięć fiskalna"));
      PostnetProtException.messages.Add(1002, new PostnetProtExceptionItem(1002, "ERR_FM_WRITE", "błąd zapisu"));
      PostnetProtException.messages.Add(1003, new PostnetProtExceptionItem(1003, "ERR_FM_UNKN", "błąd nie ujęty w specyfikacji bios"));
      PostnetProtException.messages.Add(1004, new PostnetProtExceptionItem(1004, "ERR_FM_CHKSUM_CNT", "błędne sumy kontrolne"));
      PostnetProtException.messages.Add(1005, new PostnetProtExceptionItem(1005, "ERR_FM_CTRL_BLK_0", "błąd w pierwszym bloku kontrolnym"));
      PostnetProtException.messages.Add(1006, new PostnetProtExceptionItem(1006, "ERR_FM_CTRL_BLK_1", "błąd w drugim bloku kontrolnym"));
      PostnetProtException.messages.Add(1007, new PostnetProtExceptionItem(1007, "ERR_FM_BAD_REC_ID", "błędny id rekordu"));
      PostnetProtException.messages.Add(1008, new PostnetProtExceptionItem(1008, "ERR_FM_DATA_ADDR_INIT", "błąd inicjalizacji adresu startowego"));
      PostnetProtException.messages.Add(1009, new PostnetProtExceptionItem(1009, "ERR_FM_DATA_ADDR_INITED", "adres startowy zainicjalizowany"));
      PostnetProtException.messages.Add(1010, new PostnetProtExceptionItem(1010, "ERR_FM_NU_PRESENT", "numer unikatowy już zapisany"));
      PostnetProtException.messages.Add(1011, new PostnetProtExceptionItem(1011, "ERR_FM_NU_NO_PRESENT_FSK", "brak numeru w trybie fiskalnym"));
      PostnetProtException.messages.Add(1012, new PostnetProtExceptionItem(1012, "ERR_FM_NU_WRITE", "błąd zapisu numeru unikatowego"));
      PostnetProtException.messages.Add(1013, new PostnetProtExceptionItem(1013, "ERR_FM_NU_FULL", "przepełnienie numerów unikatowych"));
      PostnetProtException.messages.Add(1014, new PostnetProtExceptionItem(1014, "ERR_FM_NU_LANG", "błędny język w numerze unikatowym"));
      PostnetProtException.messages.Add(1015, new PostnetProtExceptionItem(1015, "ERR_FM_TIN_CNT", "więcej niż jeden NIP"));
      PostnetProtException.messages.Add(1016, new PostnetProtExceptionItem(1016, "ERR_FM_READ_ONLY_NFSK", "drukarka w trybie do odczytu bez rekordu fiskalizacji"));
      PostnetProtException.messages.Add(1017, new PostnetProtExceptionItem(1017, "ERR_FM_CLR_RAM_CNT", "przekroczono liczbę zerowań RAM"));
      PostnetProtException.messages.Add(1018, new PostnetProtExceptionItem(1018, "ERR_FM_REP_DAY_CNT", "przekroczono liczbę raportów dobowych"));
      PostnetProtException.messages.Add(1019, new PostnetProtExceptionItem(1019, "ERR_FM_VERIFY_NU", "błąd weryfikacji numeru unikatowego"));
      PostnetProtException.messages.Add(1020, new PostnetProtExceptionItem(1020, "ERR_FM_VERIFY_STAT", "błąd weryfikacji statystyk z RD."));
      PostnetProtException.messages.Add(1021, new PostnetProtExceptionItem(1021, "ERR_FM_VERIFY_NVR_READ", "błąd odczytu danych z NVR do weryfikacji FM"));
      PostnetProtException.messages.Add(1022, new PostnetProtExceptionItem(1022, "ERR_FM_VERIFY_NVR_WRITE", "błąd zapisu danych z NVR do weryfikacji FM"));
      PostnetProtException.messages.Add(1023, new PostnetProtExceptionItem(1023, "ERR_FM_CTRL_BLK_2", "pamięć fiskalna jest mała 1Mb zamiast 2Mb"));
      PostnetProtException.messages.Add(1024, new PostnetProtExceptionItem(1024, "ERR_FM_DATA_ADDR_NO_INITED", "nie zainicjalizowany obszar danych w pamięci fiskalnej"));
      PostnetProtException.messages.Add(1025, new PostnetProtExceptionItem(1025, "ERR_FM_NU_FORMAT", "błędny format numeru unikatowego"));
      PostnetProtException.messages.Add(1026, new PostnetProtExceptionItem(1026, "ERR_FM_REC_BAD_CNT", "za dużo błędnych bloków w FM"));
      PostnetProtException.messages.Add(1027, new PostnetProtExceptionItem(1027, "ERR_FM_NO_BADBLK_MARKER", "błąd oznaczenia błędnego bloku"));
      PostnetProtException.messages.Add(1028, new PostnetProtExceptionItem(1028, "ERR_FM_REC_EMPTY", "rekord w pamięci fiskalnej nie istnieje - obszar pusty"));
      PostnetProtException.messages.Add(1029, new PostnetProtExceptionItem(1029, "ERR_FM_REC_DATE", "rekord w pamięci fiskalnej z datą późniejszą od poprzedniego"));
      PostnetProtException.messages.Add(1950, new PostnetProtExceptionItem(1950, "ERR_TR_TOT_OVR", "przekroczony zakres totalizerów paragonu."));
      PostnetProtException.messages.Add(1951, new PostnetProtExceptionItem(1951, "ERR_TR_PF_OVR", "wpłata formą płatności przekracza max. wpłatę."));
      PostnetProtException.messages.Add(1952, new PostnetProtExceptionItem(1952, "ERR_TR_PF_SUM_OVR", "suma form płatności przekracza max. wpłatę."));
      PostnetProtException.messages.Add(1953, new PostnetProtExceptionItem(1953, "ERR_PAYMENT_OVR", "formy płatności pokrywają już do zapłaty."));
      PostnetProtException.messages.Add(1954, new PostnetProtExceptionItem(1954, "ERR_TR_CHANGE_OVR", "wpłata reszty przekracza max. wpłatę."));
      PostnetProtException.messages.Add(1955, new PostnetProtExceptionItem(1955, "ERR_TR_CHANGE_SUM_OVR", "suma form płatności przekracza max. wpłatę."));
      PostnetProtException.messages.Add(1956, new PostnetProtExceptionItem(1956, "ERR_TR_TOTAL_OVR", "przekroczony zakres total."));
      PostnetProtException.messages.Add(1957, new PostnetProtExceptionItem(1957, "ERR_TR_FISC_OVR", "przekroczony maksymalny zakres paragonu."));
      PostnetProtException.messages.Add(1958, new PostnetProtExceptionItem(1958, "ERR_TR_PACK_OVR", "przekroczony zakres wartości opakowań."));
      PostnetProtException.messages.Add(1959, new PostnetProtExceptionItem(1959, "ERR_TR_PACK_STORNO_OVR", "przekroczony zakres wartości opakowań przy stornowaniu."));
      PostnetProtException.messages.Add(1961, new PostnetProtExceptionItem(1961, "ERR_TR_PF_REST_TOO_BIG", "wpłata reszty zbyt duża"));
      PostnetProtException.messages.Add(1962, new PostnetProtExceptionItem(1962, "ERR_TR_PF_ZERO", "wpłata formą płatności wartości 0"));
      PostnetProtException.messages.Add(1980, new PostnetProtExceptionItem(1980, "ERR_TR_DISCNT_BASE_OVR", "przekroczony zakres kwoty bazowej rabatu/narzutu"));
      PostnetProtException.messages.Add(1981, new PostnetProtExceptionItem(1981, "ERR_TR_DISCNT_AFTER_OVR", "przekroczony zakres kwoty po rabacie / narzucie"));
      PostnetProtException.messages.Add(1982, new PostnetProtExceptionItem(1982, "ERR_TR_DISCNT_CALC", "błąd obliczania rabatu/narzutu"));
      PostnetProtException.messages.Add(1983, new PostnetProtExceptionItem(1983, "ERR_TR_DISCNT_BASE_NEGATIVE_OR_ZERO", "wartość bazowa ujemna lub równa 0"));
      PostnetProtException.messages.Add(1984, new PostnetProtExceptionItem(1984, "ERR_TR_DISCNT_ZERO", "wartość rabatu/narzutu zerowa"));
      PostnetProtException.messages.Add(1985, new PostnetProtExceptionItem(1985, "ERR_TR_DISCNT_AFTER_NEGATIVE_OR_ZERO", "wartość po rabacie ujemna lub równa 0"));
      PostnetProtException.messages.Add(1990, new PostnetProtExceptionItem(1990, "ERR_TR_STORNO_NOT_ALLOWED", "Niedozwolone stornowanie towaru. Błędny stan transakcji."));
      PostnetProtException.messages.Add(1991, new PostnetProtExceptionItem(1991, "ERR_TR_DISCNT_NOT_ALLOWED", "Niedozwolony rabat/narzut. Błędny stan transakcji."));
      PostnetProtException.messages.Add(2000, new PostnetProtExceptionItem(2000, "ERR_TR_FLD_VAT", "błąd pola VAT."));
      PostnetProtException.messages.Add(2002, new PostnetProtExceptionItem(2002, "ERR_NO_HDR", "brak nagłówka"));
      PostnetProtException.messages.Add(2003, new PostnetProtExceptionItem(2003, "ERR_HDR", "zaprogramowany nagłówek"));
      PostnetProtException.messages.Add(2004, new PostnetProtExceptionItem(2004, "ERR_NO_VAT", "brak aktywnych stawek VAT."));
      PostnetProtException.messages.Add(2005, new PostnetProtExceptionItem(2005, "ERR_NO_TRNS_MODE", "brak trybu transakcji."));
      PostnetProtException.messages.Add(2006, new PostnetProtExceptionItem(2006, "ERR_TR_FLD_PRICE", "błąd pola cena ( cena <= 0 )"));
      PostnetProtException.messages.Add(2007, new PostnetProtExceptionItem(2007, "ERR_TR_FLD_QUANT", "błąd pola ilość ( ilość <= 0 )"));
      PostnetProtException.messages.Add(2008, new PostnetProtExceptionItem(2008, "ERR_TR_FLD_TOTAL", "błąd kwoty total"));
      PostnetProtException.messages.Add(2009, new PostnetProtExceptionItem(2009, "ERR_TR_FLD_TOTAL_ZERO", "błąd kwoty total, równa zero"));
      PostnetProtException.messages.Add(2010, new PostnetProtExceptionItem(2010, "ERR_TOT_OVR", "przekroczony zakres totalizerów dobowych."));
      PostnetProtException.messages.Add(2021, new PostnetProtExceptionItem(2021, "ERR_RTC_WAS_SET", "próba ponownego ustawienia zegara."));
      PostnetProtException.messages.Add(2022, new PostnetProtExceptionItem(2022, "ERR_RTC_DIFF", "zbyt duża różnica dat"));
      PostnetProtException.messages.Add(2023, new PostnetProtExceptionItem(2023, "ERR_RTC_HOUR", "różnica większa niż godzina w trybie użytkownika w trybie fiskalnym."));
      PostnetProtException.messages.Add(2024, new PostnetProtExceptionItem(2024, "ERR_RTC_BAD_FORMAT", "zły format daty (np. 13 miesiąc )"));
      PostnetProtException.messages.Add(2025, new PostnetProtExceptionItem(2025, "ERR_RTC_FM_DATE", "data wcześniejsza od ostatniego zapisu do modułu"));
      PostnetProtException.messages.Add(2026, new PostnetProtExceptionItem(2026, "ERR_RTC", "błąd zegara."));
      PostnetProtException.messages.Add(2027, new PostnetProtExceptionItem(2027, "ERR_VAT_CHNG_CNT", "przekroczono maksymalną liczbę zmian stawek VAT"));
      PostnetProtException.messages.Add(2028, new PostnetProtExceptionItem(2028, "ERR_VAT_SAME", "próba zdefiniowana identycznych stawek VAT"));
      PostnetProtException.messages.Add(2029, new PostnetProtExceptionItem(2029, "ERR_VAT_VAL", "błędne wartości stawek VAT"));
      PostnetProtException.messages.Add(2030, new PostnetProtExceptionItem(2030, "ERR_VAT_NO_ACTIVE", "próba zdefiniowania stawek VAT wszystkich nieaktywnych"));
      PostnetProtException.messages.Add(2031, new PostnetProtExceptionItem(2031, "ERR_FLD_TIN", "błąd pola NIP."));
      PostnetProtException.messages.Add(2032, new PostnetProtExceptionItem(2032, "ERR_FM_ID", "błąd numeru unikatowego pamięci fiskalnej."));
      PostnetProtException.messages.Add(2033, new PostnetProtExceptionItem(2033, "ERR_FISC_MODE", "urządzenie w trybie fiskalnym."));
      PostnetProtException.messages.Add(2034, new PostnetProtExceptionItem(2034, "ERR_NO_FISC_MODE", "urządzenie w trybie niefiskalnym."));
      PostnetProtException.messages.Add(2035, new PostnetProtExceptionItem(2035, "ERR_TOT_NOT_ZERO", "niezerowe totalizery."));
      PostnetProtException.messages.Add(2036, new PostnetProtExceptionItem(2036, "ERR_READ_ONLY", "urządzenie w stanie tylko do odczytu."));
      PostnetProtException.messages.Add(2037, new PostnetProtExceptionItem(2037, "ERR_NO_READ_ONLY", "urządzenie nie jest w stanie tylko do odczytu."));
      PostnetProtException.messages.Add(2038, new PostnetProtExceptionItem(2038, "ERR_TRNS_MODE", "urządzenie w trybie transakcji."));
      PostnetProtException.messages.Add(2039, new PostnetProtExceptionItem(2039, "ERR_TOT_ZERO", "zerowe totalizery."));
      PostnetProtException.messages.Add(2040, new PostnetProtExceptionItem(2040, "ERR_CURR_CALC", "błąd obliczeń walut, przepełnienie przy mnożeniu lub dzieleniu."));
      PostnetProtException.messages.Add(2041, new PostnetProtExceptionItem(2041, "ERR_TR_END_VAL_0", "próba zakończenia pozytywnego paragonu z wartością 0"));
      PostnetProtException.messages.Add(2042, new PostnetProtExceptionItem(2042, "ERR_REP_PER_DATE_FORMAT_FROM", "błędy format daty początkowej"));
      PostnetProtException.messages.Add(2043, new PostnetProtExceptionItem(2043, "ERR_REP_PER_DATE_FORMAT_TO", "błędy format daty końcowej"));
      PostnetProtException.messages.Add(2044, new PostnetProtExceptionItem(2044, "ERR_REP_PER_CURR_MONTH", "próba wykonania raportu miesięcznego w danym miesiącu"));
      PostnetProtException.messages.Add(2045, new PostnetProtExceptionItem(2045, "ERR_REP_PER_DATE_START_GT_CURR", "data początkowa późniejsza od bieżącej daty"));
      PostnetProtException.messages.Add(2046, new PostnetProtExceptionItem(2046, "ERR_REP_PER_DATE_END_GT_FISK", "data końcowa wcześniejsza od daty fiskalizacji"));
      PostnetProtException.messages.Add(2047, new PostnetProtExceptionItem(2047, "ERR_REP_PER_NUM_ZERO", "numer początkowy lub końcowy równy zero"));
      PostnetProtException.messages.Add(2048, new PostnetProtExceptionItem(2048, "ERR_REP_PER_NUM_FROM_GT_END", "numer początkowy większy od numeru końcowego"));
      PostnetProtException.messages.Add(2049, new PostnetProtExceptionItem(2049, "ERR_REP_PER_NUM_TOO_BIG", "numer raportu zbyt duży"));
      PostnetProtException.messages.Add(2050, new PostnetProtExceptionItem(2050, "ERR_REP_PER_DATE_END_GT_START", "data początkowa późniejsza od daty końcowej"));
      PostnetProtException.messages.Add(2051, new PostnetProtExceptionItem(2051, "ERR_REP_TXT_NO_MEM", "brak pamięci w buforze tekstów."));
      PostnetProtException.messages.Add(2052, new PostnetProtExceptionItem(2052, "ERR_TR_NO_MEM", "brak pamięci w buforze transakcji"));
      PostnetProtException.messages.Add(2054, new PostnetProtExceptionItem(2054, "ERR_TR_END_PAYMENT", "formy płatności nie pokrywają kwoty do zapłaty lub reszty"));
      PostnetProtException.messages.Add(2055, new PostnetProtExceptionItem(2055, "ERR_LINE", "błędna linia"));
      PostnetProtException.messages.Add(2056, new PostnetProtExceptionItem(2056, "ERR_EMPTY_TXT", "tekst pusty"));
      PostnetProtException.messages.Add(2057, new PostnetProtExceptionItem(2057, "ERR_SIZE", "przekroczony rozmiar"));
      PostnetProtException.messages.Add(2058, new PostnetProtExceptionItem(2058, "ERR_LINE_CNT", "błędna liczba linii."));
      PostnetProtException.messages.Add(2060, new PostnetProtExceptionItem(2060, "ERR_TR_BAD_STATE", "błędny stan transakcji"));
      PostnetProtException.messages.Add(2062, new PostnetProtExceptionItem(2062, "ERR_PRN_START", "jest wydrukowana część jakiegoś dokumentu"));
      PostnetProtException.messages.Add(2063, new PostnetProtExceptionItem(2063, "ERR_PAR", "błąd parametru"));
      PostnetProtException.messages.Add(2064, new PostnetProtExceptionItem(2064, "ERR_FTR_NO_HDR", "brak rozpoczęcia wydruku lub transakcji"));
      PostnetProtException.messages.Add(2067, new PostnetProtExceptionItem(2067, "ERR_PRN_CFG_SET", "błąd ustawień konfiguracyjnych wydruków / drukarki"));
      PostnetProtException.messages.Add(2070, new PostnetProtExceptionItem(2070, "ERR_WRONG_MAINTENANCE_TIME", "Data przeglądu wcześniejsza od systemowej"));
      PostnetProtException.messages.Add(2101, new PostnetProtExceptionItem(2101, "ERR_DF_DB_OVR", "Zapełnienie bazy"));
      PostnetProtException.messages.Add(2102, new PostnetProtExceptionItem(2102, "ERR_DF_DB_VAT_INACTIVE", "Stawka nieaktywna"));
      PostnetProtException.messages.Add(2103, new PostnetProtExceptionItem(2103, "ERR_DF_DB_VAT_INVALID", "Nieprawidłowa stawka VAT"));
      PostnetProtException.messages.Add(2104, new PostnetProtExceptionItem(2104, "ERR_DF_DB_NAME", "Błąd nazwy"));
      PostnetProtException.messages.Add(2105, new PostnetProtExceptionItem(2105, "ERR_DF_DB_NAME_VAT", "Błąd przypisania stawki"));
      PostnetProtException.messages.Add(2106, new PostnetProtExceptionItem(2106, "ERR_DF_DB_LOCKED", "Towar został zablokowany przez drukarkę"));
      PostnetProtException.messages.Add(2107, new PostnetProtExceptionItem(2107, "ERR_DF_DB_NAME_NOT_FOUND", "Nie znaleziono w bazie drukarkowej"));
      PostnetProtException.messages.Add(2108, new PostnetProtExceptionItem(2108, "ERR_DF_DB_NOT_OVR", "baza nie jest zapełniona"));
      PostnetProtException.messages.Add(2501, new PostnetProtExceptionItem(2501, "ERR_FORM_ID", "Błędny identyfikator raportu"));
      PostnetProtException.messages.Add(2502, new PostnetProtExceptionItem(2502, "ERR_FORM_LINE_NO", "Błędny identyfikator linii raportu"));
      PostnetProtException.messages.Add(2503, new PostnetProtExceptionItem(2503, "ERR_FORM_HDR_NO", "Błędny identyfikator nagłówka raportu"));
      PostnetProtException.messages.Add(2504, new PostnetProtExceptionItem(2504, "ERR_FORM_PAR_CNT", "Zbyt mało parametrów raportu"));
      PostnetProtException.messages.Add(2505, new PostnetProtExceptionItem(2505, "ERR_FORM_NOT_STARTED", "Raport nie rozpoczęty"));
      PostnetProtException.messages.Add(2506, new PostnetProtExceptionItem(2506, "ERR_FORM_STARTED", "Raport rozpoczęty"));
      PostnetProtException.messages.Add(2507, new PostnetProtExceptionItem(2507, "ERR_FORM_CMD", "Błędny identyfikator komendy"));
      PostnetProtException.messages.Add(2521, new PostnetProtExceptionItem(2521, "ERR_DB_REP_PLU_ACTIVE", "Raport już rozpoczęty"));
      PostnetProtException.messages.Add(2522, new PostnetProtExceptionItem(2522, "ERR_DB_REP_PLU_NOT_ACTIVE", "Raport nie rozpoczęty"));
      PostnetProtException.messages.Add(2523, new PostnetProtExceptionItem(2523, "ERR_DB_REP_PLU_VAT_ID", "Błędna stawka VAT"));
      PostnetProtException.messages.Add(2532, new PostnetProtExceptionItem(2532, "ERR_FV_COPY_CNT", "Błędna liczba kopii faktur"));
      PostnetProtException.messages.Add(2600, new PostnetProtExceptionItem(2600, "ERR_DISCNT_TYPE", "Błędny typ rabatu/narzutu"));
      PostnetProtException.messages.Add(2601, new PostnetProtExceptionItem(2601, "ERR_TR_DISCNT_VALUE", "wartość rabatu/narzutu spoza zakresu"));
      PostnetProtException.messages.Add(2701, new PostnetProtExceptionItem(2701, "ERR_VAT_ID", "Błąd identyfikatora stawki podatkowej."));
      PostnetProtException.messages.Add(2702, new PostnetProtExceptionItem(2702, "ERR_FTRLN_ID", "Błędny identyfikator dodatkowej stopki."));
      PostnetProtException.messages.Add(2703, new PostnetProtExceptionItem(2703, "ERR_FTRLN_CNT", "Przekroczona liczba dodatkowych stopek."));
      PostnetProtException.messages.Add(2704, new PostnetProtExceptionItem(2704, "ERR_ACC_LOW", "Zbyt słaby akumulator."));
      PostnetProtException.messages.Add(2705, new PostnetProtExceptionItem(2705, "ERR_PF_TYPE", "Błędny identyfikator typu formy płatności."));
      PostnetProtException.messages.Add(2801, new PostnetProtExceptionItem(2801, "ERR_DISCNT_VERIFY", "Błąd weryfikacji wartości rabatu/narzutu"));
      PostnetProtException.messages.Add(2802, new PostnetProtExceptionItem(2802, "ERR_LNTOT_VERIFY", "Błąd weryfikacji wartości linii sprzedaży"));
      PostnetProtException.messages.Add(2803, new PostnetProtExceptionItem(2803, "ERR_PACKTOT_VERIFY", "Błąd weryfikacji wartości opakowania"));
      PostnetProtException.messages.Add(2804, new PostnetProtExceptionItem(2804, "ERR_CURRTOT_VERIFY", "Błąd weryfikacji wartości formy płatności"));
      PostnetProtException.messages.Add(2805, new PostnetProtExceptionItem(2805, "ERR_ENDTOT_VERIFY", "Błąd weryfikacji wartości fiskalnej"));
      PostnetProtException.messages.Add(2806, new PostnetProtExceptionItem(2806, "ERR_ENDPACKPLUS_VERIFY", "Błąd weryfikacji wartości opakowań dodatnich"));
      PostnetProtException.messages.Add(2807, new PostnetProtExceptionItem(2807, "ERR_ENDPACKMINUS_VERIFY", "Błąd weryfikacji wartości opakowań ujemnych"));
      PostnetProtException.messages.Add(2808, new PostnetProtExceptionItem(2808, "ERR_ENDPAYMENT_VERIFY", "Błąd weryfikacji wartości wpłaconych form płatności"));
      PostnetProtException.messages.Add(2809, new PostnetProtExceptionItem(2809, "ERR_ENDCHANGE_VERIFY", "Błąd weryfikacji wartości reszt"));
      PostnetProtException.messages.Add(2851, new PostnetProtExceptionItem(2851, "ERR_STORNO_QNT", "Błąd stornowania, błędna ilość"));
      PostnetProtException.messages.Add(2852, new PostnetProtExceptionItem(2852, "ERR_STORNO_AMT", "Błąd stornowania, błędna wartość"));
      PostnetProtException.messages.Add(2903, new PostnetProtExceptionItem(2903, "ERR", "Pamięć podręczna kopii elektronicznej zawiera zbyt dużą ilość danych"));
    }

    public PostnetProtException(string msg)
    {
      base.\u002Ector(msg);
      this.message = (__Null) msg;
      this.type = (__Null) 0;
    }

    public PostnetProtException(int num, string msg, OFPExceptionType excType)
    {
      base.\u002Ector(msg);
      this.number = (__Null) num;
      this.message = (__Null) msg;
      this.type = (__Null) excType;
    }

    public PostnetProtException(int num)
    {
      base.\u002Ector("Drukarka zwrócila nieznany błąd: " + (object) num);
      if (PostnetProtException.messages.ContainsKey(num))
        this.message = (__Null) (PostnetProtException.messages[num].Mnemonik + " " + PostnetProtException.messages[num].Description);
      this.number = (__Null) num;
      this.type = (__Null) 0;
    }

    public PostnetProtException(int num, string currentCommand)
    {
      base.\u002Ector("Drukarka zwrócila nieznany błąd: " + (object) num + " podczas wykonywania polecenia:" + currentCommand);
      if (PostnetProtException.messages.ContainsKey(num))
        this.message = (__Null) ("Błąd podczas wykonywania polecenia " + currentCommand + ": " + PostnetProtException.messages[num].Mnemonik + " " + PostnetProtException.messages[num].Description);
      this.number = (__Null) num;
      this.type = (__Null) 0;
    }
  }
}
