import { observer } from 'mobx-react';
import React from 'react'
import { useContext } from 'react';
import { StoreContext } from '../stores/StoreContext';

export default observer(function Example() {
    const {user, authToken} = useContext(StoreContext).userStore;
    
const handleClick = ()=>{
  
}

    return (
        <div>
            łololo
            <button onClick={handleClick}>Klick</button>
            <br/>
            {user.username}
            <br/>
            {authToken}
        </div>
    )
});
