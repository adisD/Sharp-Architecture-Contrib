=============================================
Additional Contributions to Sharp Architcture
=============================================
This is an early beta containing additional contributions to Sharp Architecture.
This project is currently sharing a home with its parent, Sharp Architecture, at
http://code.google.com/p/sharp-architecture/.

URLs of Interest:

- Downloads: http://github.com/codai/Sharp-Architecture-Contrib/downloads
- GitHub Repository: http://github.com/codai/Sharp-Architecture-Contrib


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

The existing SharpArch.Wcf libraries will be moving to SharpArchContrib shortly.

=============================================
Documentation
=============================================
The documentation wiki is http://wiki.sharparchitecture.net/SharpArchContrib.ashx.  
SharpArchContrib is currently a work in process so the documentation may not always be 
up to date.  See VersionHistory.txt for information about changes made in each
release and migration issues for existing projects.

=============================================
Getting Help
=============================================
You can post questions, suggestions and comments about SharpArchContrib
to the Sharp Architecture group at http://groups.google.com/group/sharp-architecture.

============================================
License
============================================
Licensed under the New BSD license.  See license.txt for details.

=============================================
Building
=============================================
Use build.bat from the command-line or click on ClickToBuild.bat in
Windows Explorer.  Project outputs will be placed in a the BuildOutput
folder.

=============================================
Additional Dependencies
=============================================
SharpArchContrib currently depends on the master branch of SharpArch. The correct SharpArch 
binaries and soure will be maintained on the SharpArchContrib page for convinience during the beta.