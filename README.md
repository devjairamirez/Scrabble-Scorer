# Scrabble-Scorer

Scrabble-Scorer is a cross-platform mobile application built with Xamarin.Forms to help track, score, and manage Scrabble games. It provides an intuitive interface for recording player scores, managing game sessions, and viewing high scores.

## Features

- **Game Management:** Start, track, and end Scrabble games. Each game records players, start/end times, and scores.
- **Player Scoring:** Add and update scores for each player per round. Automatically calculates total scores.
- **Score History:** View high scores and previous games.
- **Persistent Storage:** Uses SQLite for saving games and player data locally.
- **Cross-Platform:** Runs on Android and iOS devices.

## Technology Stack

- **Xamarin.Forms:** UI framework for building native mobile apps.
- **SQLite:** Local database for storing games and scores.
- **MVVM Architecture:** Clean separation using ViewModels for data binding and business logic.

## Project Structure

- `/ScrabbleScorer/ScrabbleScorer/Models`: Data models for `Game` and `Player`.
- `/ScrabbleScorer/ScrabbleScorer/ViewModels`: MVVM ViewModels for game logic, high scores, and UI state.
- `/ScrabbleScorer/ScrabbleScorer/Services`: DataStore services for managing game/player persistence.
- `/ScrabbleScorer/ScrabbleScorer/Helper`: Helper classes for constants and session data.
- `/ScrabbleScorer/ScrabbleScorer/Views`: UI pages for high scores, game details, and more.
- `/ScrabbleScorer/ScrabbleScorer.Android` & `/ScrabbleScorer/ScrabbleScorer.iOS`: Platform-specific startup code.

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/devjairamirez/Scrabble-Scorer.git
   ```
2. **Open in Visual Studio** (with Xamarin support).
3. **Build and run** on Android or iOS simulator/device.

### Navigation and Usage

- The app uses Xamarin Shell for navigation, providing tabs, flyout menus, and integrated search.
- Explore `AppShell.xaml` to see the navigation structure.
- To learn more about Shell, visit: [Xamarin Shell Introduction](https://docs.microsoft.com/xamarin/xamarin-forms/app-fundamentals/shell/introduction)

## License

This project is licensed under the MIT License.

---

Contributions and feedback are welcome!