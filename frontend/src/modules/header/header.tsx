import { FC } from 'react';

// import { Link } from 'react-router-dom';
import styles from './header.module.scss';

const Header: FC = () => {
    return (
        <header className={styles.header}>
            <div className={styles.headerContent}>
                {/* <Link to={`/profile`}>
                    <img src={Logo} alt='logo' />
                </Link> */}

                link 1
                link 2
                link 3
                link 4
                link 5
                
                <div />
            </div>
        </header>
    );
};

export { Header };
