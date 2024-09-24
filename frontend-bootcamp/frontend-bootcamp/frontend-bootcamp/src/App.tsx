import './App.css'
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link
} from "react-router-dom";
import Home from "./pages/home"
import Authors from "./pages/authors"
import Author from "./pages/author"
import AuthorForm from "./pages/authorform"
import BookForm from "./pages/bookform"
function App() {
  return (
    <>
    <Router>
      <nav className="navbar">
        <div className='brand'>
          <span className='title'><strong>Booktopia</strong></span>
        </div>
        <div className='nav-links'>
          <Link path="/" to="/">
            <button id='home-button'>
              Home
            </button>
          </Link>
          <Link path="/authors" to="/authors">
            <button id='authors-button'>
              Authors
            </button>
          </Link>
        </div>
      </nav>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/authors" element={<Authors />} />
        <Route path="/authors/:id" element={<Author/>} />
        <Route path="/authors/form" element={<AuthorForm />} />
        <Route path="/authors/form/:id" element={<AuthorForm />} />
        <Route path="/books/form/:authorId" element={<BookForm />} />
        <Route path="/books/form/:authorId/:id" element={<BookForm />} />
      </Routes>
    </Router>
    </>
  )
}

export default App
