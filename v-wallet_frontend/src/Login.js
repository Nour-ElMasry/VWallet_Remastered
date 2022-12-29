import React, {useState} from "react";

const Login = (props) => {
    const [user, setUser] = useState({
        username: "",
        password: ""
    });

    const handleChange = (event) => {
        const {name, value} = event.target;
        setUser((prev) => ({...prev, [name]: value}));
    }

    return <section className="login flex flex-jc-c flex-ai-c">
            <div className="login-container">
                <h1>Log In</h1>
                <div className="flex">
                    <input name="username" type="text" placeholder="Username" value={user.username} onChange={handleChange} autoComplete="off" />
                    <input name="password" type="password" placeholder="Password" value={user.password} onChange={handleChange} />
                    <button onClick={(e) => {
                        e.preventDefault();
                        props.check(user.username, user.password)
                    } }>Sign in</button>
                </div>
            </div>
        </section>
}

export default Login;