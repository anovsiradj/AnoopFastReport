
upaya untuk membuat "ReportConsole.exe" menggunakan FastReport opensource.
untuk sekarang, pengembangan berhenti karena beberapa hal.

1) koneksi database pada designer bermasalah
https://github.com/FastReports/FastReport/issues/701
2) skrip `conn.CreateAllTables()` masih error
3) skrip `report.Prepare()` masih error
4) beberapa lewat designer bisa, tapi lewat script tidak bisa.

intinya semua yang berhubungan dengan koneksi database,
masih bermasalah.
