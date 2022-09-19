import logo from './logo.svg';
import './App.css';

import {BrowserRouter, Route, Switch,NavLink} from 'react-router-dom';
import MoviesList from './moviesList';
import Movie from './movie';

function App() {
  return (
    <>
     <BrowserRouter>
     {/* <div className="App container">  
       <nav className="navbar navbar-expand-sm bg-light navbar-dark">
         <ul className="navbar-nav">
           <li className="nav-item- m-1">
             <NavLink className="btn btn-light btn-outline-primary" to="/moviesList">
               Home
             </NavLink>
           </li>
           <li className="nav-item- m-1">
             <NavLink className="btn btn-light btn-outline-primary" to="/moviesList">
             moviesList
             </NavLink>
             </li>
         </ul>
     </nav>     </div> */}
     <MoviesList/>
       <Switch>
         <Route path='/moviesList' component={MoviesList}/>
       <Route path='/movie:id' component={Movie}/>
     </Switch>

    </BrowserRouter>
    </>
  );
}

export default App;
