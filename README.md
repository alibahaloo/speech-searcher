speech-searcher
===============

What is it?
It's a simple windows application that lets you search for a topic using your voice. Speak your desired topic into the microphone and tell the application where do you want to search this one, and result will appear as you finished speaking. The aim and goal of this project was to investigate possible ways to increase the accuracy of speech recognition in windows, this application was developed as a prototype first and the was extended for general usage. So the final product is considered to be the most accurate one. The application is able to understand the following search engines: Google / Yahoo / Wikipedia / Amazon

How does it work?
This application is using a stand-alone speech recognizer that is initiated by the application itself. That is, instead of using the Windows Speech Recognizer component, we designed a separate and sophisticated engine. It is loaded with a language model(grammar) that makes the application able to understand the user's requests.

Development:
The application is developed under .net framework 4.0. It is using the Speech.Recognition namespace rather that using the Speech API. An in-process recognizer is designed rather than using the shared one. In designing the language model, SRGS format is used, and an external approach was applied.

Versions
V1.0: Features:

Original Features.
V1.6: Updated version of Speech Searcher. Added Features:
- Choose between "Go to" engine and "Searcher" engine.
- Go To eninge: Enables you to access Windows's default folders with your voice
- Music / Documents / Desktop / Pictures / Videos / Computer / Tools (Admin Tools) / Programs
- Add word to dictionary: Add words to dictation grammar for later recognition; Improves accuracy.
- No msgbox from domain analyzer and spelling engines.
- Confirmation in status label instead of msgbox: Notice the status label for any change.

V1.8: Updated version of Speech Searcher. Added Features:
- Progress bar to indicate the SR is running or not running.
