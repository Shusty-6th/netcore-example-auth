import React, {FC} from 'react'
import Button from '@material-ui/core/Button';
import { useHistory } from 'react-router';

type LinkRouteButtonProps = { 
    children: string
 };

const LinkRouteButton : FC<{to:string}> = ({children, to}) => {
    const history = useHistory();
    const location = {
        pathname: to,
    };

    const handleOnClick = ()=>{
        history.push(location);
    }

    return (
        <Button color="inherit" onClick={handleOnClick}>
            {children}
        </Button>
    )
}

export default LinkRouteButton

