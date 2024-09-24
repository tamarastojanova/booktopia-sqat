import '../assets/home.css';

function Home() { 

return (
    <>
    <div className="cover">
      <div className="cover-text">
        <h1>Welcome to Booktopia</h1>
        <p>Your ultimate hub for managing books and authors with ease. Discover, organize, and explore your literary world effortlessly!</p>
        <a href="/authors" id="get-started-button" 
          className="btn btn-get-started btn-lg text-white" 
          style={{width: '18%', padding: '5px 5px'}}>Get Started</a>
      </div>
    </div>
    </>
  )
}

export default Home