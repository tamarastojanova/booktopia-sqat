import { useState, useEffect } from 'react';
import '../assets/authors.css'
import {
    Link
  } from "react-router-dom";
import fetcher from '../Fetcher';

function authors() {
    const [authors, setAuthors] = useState<Author[]>([])
    const [query, setQuery] = useState<string>('')
    const [filteredAuthors, setFilteredAuthors] = useState<Author[]>([])

    interface Author {
        id : number
        firstName : string
        lastName : string
        imageUrl : string
    }
    useEffect(() => {
        fetchAuthors()
        filterAuthors()
    }, [authors])

    function fetchAuthors() {
        fetcher<Author[]>('https://localhost:44371/authors')
        .then(data => setAuthors(data))
    }
    function displayAuthors() {
        return filteredAuthors.map(author => {
            return (
                <div className='card' key={author.id}>
                    <img className='author-img' src={author.imageUrl} alt={author.firstName} />
                    <h2>{author.firstName} {author.lastName}</h2>
                    <div className='btn-group'>
                        <Link to={`/authors/${author.id}`}><button className='view-btn btn'>View</button></Link>
                        <Link to={`/authors/form/${author.id}`}><button className='edit-btn btn'>Edit</button></Link>
                        <button className='delete-btn btn' onClick={() => deleteAuthor(author.id)}>Delete</button>
                    </div>
                </div>
            )
        })
    }
    function deleteAuthor(id: number) {
        fetcher(`https://localhost:44371/authors/${id}`, 'DELETE')
        .then(() => fetchAuthors())
    }
    function handleChange(event : any) {
        const value = event.target.value;
        setQuery(value)
    }
    function filterAuthors() {
        if (query === '') {
            setFilteredAuthors(authors)
        }
        else {
            setFilteredAuthors(authors.filter(author => (author.firstName + ' ' + author.lastName).toLowerCase().includes(query.toLowerCase())))
        }
    }

  return (
    <div>
        <div className='input-container ic1'>
    <input id='search' className='input' type="text" name="query" value={query}
            onChange={handleChange}/>
            <div className='cut'></div>
            <label className='placeholder'>Search</label>
    </div>
        <h1>Authors</h1>
        <Link to='/authors/form'><button className='add-btn btn'>Add Author</button></Link>
        <div className='card-group'>
            {displayAuthors()}
        </div>
    </div>
  );
}
export default authors;