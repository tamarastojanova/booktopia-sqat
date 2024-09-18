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
      <nav>
        <span className='brand'>
        <span className='title'><strong>Library</strong></span>
        </span>
        <span className='nav-links'>
        <Link path="/" to="/">
        <button>
          Home
        </button>
        </Link>
        <Link path="/authors" to="/authors">
        <button>
          Authors
        </button>
        </Link>
        </span>
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
