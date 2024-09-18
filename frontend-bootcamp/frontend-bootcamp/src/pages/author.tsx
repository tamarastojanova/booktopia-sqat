import { useState, useEffect } from 'react';
import {
    Link,
    useParams
  } from "react-router-dom";
import '../assets/authors.css'
import fetcher from '../Fetcher'

function Author() {
    const { id } = useParams<string>();
    const [author, setAuthor] = useState<Author>()
    const [books, setBooks] = useState<Book[]>([])

    interface Author {
        id : number
        firstName : string
        lastName : string
        imageUrl : string
    }
    interface Book {
        id : number
        title : string
        yearPublished : number
        authorId : number
    }

    useEffect(() => {
        fetchInfo()
    })

    function fetchInfo() {
        fetcher<Author>(`https://localhost:44371/authors/${id}`)
        .then(data => setAuthor(data))
        fetcher<Book[]>(`https://localhost:44371/authors/${id}/books`)
        .then(data => setBooks(data))
    }

    function displayAuthor() {
        return (
            <div>
                <Link to={`/books/form/${author?.id}`}><button className='add-btn btn'>Add Book</button></Link>
            <div className='card' key={author?.id}>
                <img className='author-img' src={author?.imageUrl} alt={author?.firstName} />
                <h2>{author?.firstName} {author?.lastName}</h2>
            </div>
                <h2>Books</h2>
                <div className='card-group'>
                    {displayBooks()}
                </div>
            </div>
        )
    }

    function deleteBook(id: number) {
        fetcher(`https://localhost:44371/authors/${author?.id}/books/${id}`, 'DELETE')
        .then(() => fetchInfo())
    }
    function displayBooks() {
        return books.map(book => {
            return (
                <div className='book-card' key={book.id}>
                    <h4>{book.title}<br/>Year published: {book.yearPublished}</h4>
                    <div className='btn-group'>
                        <Link to={`/books/form/${book.authorId}/${book.id}`}><button className='edit-btn btn'>Edit</button></Link>
                        <button className='delete-btn btn' onClick={() => deleteBook(book.id)}>Delete</button>
                    </div>
                </div>
            )
        }
        )
    }

    return (
        <>
        <div>{displayAuthor()}</div>
        </>
      )
    }
    
    export default Author