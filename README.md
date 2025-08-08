Dynamic Brightness Adjuster 😎

Welcome to my personal timepass project!
This Windows app automatically tweaks your screen brightness depending on what app you’re using, so your eyes can chill while you work, browse, or binge.

What Does It Do?
Watches what app you’re using (Excel, VS Code, Chrome, etc.)
Changes your screen brightness to a level you set for each app
Let's you create custom brightness profiles (Low, Medium, High, etc.)
Let's you set up rules so, for example, Excel is always dim, VS Code is always bright, and Chrome is whatever you want
Saves all your settings so you don’t have to redo them every time
Has a global ON/OFF switch so you can pause the magic anytime

How Do I Use It?
1. Run the app as Administrator
(Needed for brightness control. Right-click > Run as administrator)

2. Set up your profiles
  -Use the slider to pick a brightness level
  -Click “Add Profile” to save it with a name

3. Set up your rules

  -Click the “Rules” button
  -Add a rule for each app you want to control
    >App Pattern: The process name (e.g., EXCEL.EXE, Code.exe, chrome.exe)
    >Title Pattern: Usually just * (matches any window), or use part of the window title for more specific control
    >Profile: Pick the brightness profile you want for that app

4. Switch between apps
  -The app will automatically change your brightness when you switch windows (Alt+Tab, click, etc.)
  Chill out

5. Chill out
  -Your eyes will thank you!

----
Example Rules

App	App Pattern	Title Pattern	Profile
VS Code	Code.exe	*	High
Excel	EXCEL.EXE	*	Low Comfort
Chrome	chrome.exe	YouTube	Medium


Pro Tips:- 
-The status label at the top shows you what app and window you're on, which helps when setting up new rules.

-Default brightness is used for any app that doesn't have a specific rule.

-The delay setting helps avoid screen flicker when switching windows quickly.

-You can use an asterisk * as a wildcard to match any string in your patterns.

-------
License:-
Do whatever you want.
