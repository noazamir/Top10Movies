
import {BrowserRouter, Route, Switch,NavLink} from 'react-router-dom';
import MoviesList from './moviesList';
import React, { useState, useEffect } from 'react';

export default function Movie() {


    useEffect(() => { 
       
            console.log("enter");

      }, []);
  return (
    <>
    <h1>hi</h1>
    <h1>by</h1>
    <p>wow!!!</p>
    <p>hello</p>
    </>
    
  );
}

