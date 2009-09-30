Additional Contributions to Sharp Architcture
=============================================
This is an early beta containing additional contributions to Sharp Architecture.
As such, documentation is very limited right now.

Contact Tom Cabanski at tom@cabanski.com for more information.

=============================================
Building
=============================================
Use build.bat from the command-line or click on ClickToBuild.bat in
Windows Explorer.  Project outputs will be placed in a a BuildOutput
folder.

=============================================
Primary Features
=============================================
All of the following features rely on PostSharp AOP (injection of
code at compile-time based on attributes placed in your code):

- Transaction and UnitOfWork (session and transaction per unit of work)
  for all application types including Windows GUI, service and command-line.
  Supports either System.Transaction or Nhibernate transactions based on
  Castle Windsor configuration.
- log4net logging.  Exception logging can be extended via interface and
  Castle Windsor.
