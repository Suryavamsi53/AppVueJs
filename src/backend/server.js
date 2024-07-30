const express = require('express');
const bodyParser = require('body-parser');
const apiRoutes = require('./api');
const app = express();
const port = 3000;

// Middleware
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// Routes
app.use('/api', apiRoutes);

// Root endpoint
app.get('/', (req, res) => {
  res.send('Welcome to the VuejsApp API!');
});

// Start the server
app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});

































// const express = require('express');
// const sql = require('mssql/msnodesqlv8');
// const cors = require('cors');
// const app = express();
// const port = 3012; // Adjusted port number if needed

// app.use(cors());
// app.use(express.json());

// // MSSQL configuration
// const config = {
//     server: 'localhost\\SQLEXPRESS', // Adjust server name if necessary
//     database: 'WebApp',
//     driver: 'msnodesqlv8',
//     options: {
//         trustServerCertificate: true,
//         trustedConnection: true,
//         enableArithAbort: true,
//         integratedSecurity: true, // Use integrated security (Windows Authentication)
//     },
// };

// // Connect to the database
// sql.connect(config).then(pool => {
//     if (pool.connected) {
//         console.log('Connected to MSSQL');
//     }

//     // CRUD operations for Lookups table

//    // Get all lookups
// app.get('/lookups', async (req, res) => {
//     try {
//         const pool = await sql.connect(config);
//         const result = await pool.request().query('SELECT * FROM lookup_table');
//         res.status(200).json(result.recordset);
//     } catch (err) {
//         res.status(500).send(err.message);
//     }
// });

// // Get lookup by ID
// app.get('/lookups/:id', async (req, res) => {
//     const id = req.params.id;
//     try {
//         const pool = await sql.connect(config);
//         const result = await pool.request()
//             .input('lookup_id', sql.Int, id)
//             .query('SELECT * FROM lookup_table WHERE lookup_id = @lookup_id');
//         res.status(200).json(result.recordset[0]);
//     } catch (err) {
//         res.status(500).send(err.message);
//     }
// });

// // Create new lookup
// app.post('/lookups', async (req, res) => {
//     const { lookup_type, lookup_desc, is_active } = req.body;
//     try {
//         const pool = await sql.connect(config);
//         const result = await pool.request()
//             .input('lookup_type', sql.VarChar(50), lookup_type)
//             .input('lookup_desc', sql.VarChar(255), lookup_desc)
//             .input('is_active', sql.Char(1), is_active)
//             .query('INSERT INTO lookup_table (lookup_type, lookup_desc, is_active) VALUES (@lookup_type, @lookup_desc, @is_active)');
//         res.status(201).send('Lookup created successfully');
//     } catch (err) {
//         res.status(500).send(err.message);
//     }
// });

// // Update lookup by ID
// app.put('/lookups/:id', async (req, res) => {
//     const id = req.params.id;
//     const { lookup_type, lookup_desc, is_active } = req.body;
//     try {
//         const pool = await sql.connect(config);
//         const result = await pool.request()
//             .input('lookup_id', sql.Int, id)
//             .input('lookup_type', sql.VarChar(50), lookup_type)
//             .input('lookup_desc', sql.VarChar(255), lookup_desc)
//             .input('is_active', sql.Char(1), is_active)
//             .query('UPDATE lookup_table SET lookup_type = @lookup_type, lookup_desc = @lookup_desc, is_active = @is_active WHERE lookup_id = @lookup_id');
//         res.status(200).send('Lookup updated successfully');
//     } catch (err) {
//         res.status(500).send(err.message);
//     }
// });

// // Delete lookup by ID
// app.delete('/lookups/:id', async (req, res) => {
//     const id = req.params.id;
//     try {
//         const pool = await sql.connect(config);
//         const result = await pool.request()
//             .input('lookup_id', sql.Int, id)
//             .query('DELETE FROM lookup_table WHERE lookup_id = @lookup_id');
//         res.status(200).send('Lookup deleted successfully');
//     } catch (err) {
//         res.status(500).send(err.message);
//     }
// });

//     // CRUD operations for Account_table

//     // GET all accounts
//     app.get('/accounts', async (req, res) => {
//         try {
//             const result = await pool.request().query('SELECT * FROM Account_table');
//             res.json(result.recordset);
//         } catch (err) {
//             res.status(500).send(err.message);
//         }
//     });

//     // POST a new account
//     app.post('/accounts', async (req, res) => {
//         const { AccountNumber, AccountStatus_id } = req.body;
//         console.log('Received request to add account:', req.body); // Log request body for debugging
//         try {
//             const result = await pool.request()
//                 .input('AccountNumber', sql.VarChar(50), AccountNumber)
//                 .input('AccountStatus_id', sql.Int, AccountStatus_id)
//                 .query('INSERT INTO Account_table (AccountNumber, AccountStatus_id) VALUES (@AccountNumber, @AccountStatus_id); SELECT SCOPE_IDENTITY() AS AccId;');
//             const newAccountId = result.recordset[0].AccId;
//             const newAccount = { AccId: newAccountId, AccountNumber, AccountStatus_id };
//             console.log('Account added successfully'); // Log success message
//             res.status(201).send(newAccount);
//         } catch (err) {
//             console.error('Error adding account:', err); // Log detailed error message
//             res.status(500).send(err.message);
//         }
//     });

//     // PUT (update) an account by ID
//     app.put('/accounts/:id', async (req, res) => {
//         const { id } = req.params;
//         const { AccountNumber, AccountStatus_id } = req.body;
//         try {
//             await pool.request()
//                 .input('AccId', sql.Int, id)
//                 .input('AccountNumber', sql.VarChar(50), AccountNumber)
//                 .input('AccountStatus_id', sql.Int, AccountStatus_id)
//                 .query('UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id WHERE AccId = @AccId');
//             res.send('Account updated');
//         } catch (err) {
//             res.status(500).send(err.message);
//         }
//     });

//     // DELETE an account by ID
//     app.delete('/accounts/:id', async (req, res) => {
//         const { id } = req.params;
//         try {
//             await pool.request()
//                 .input('AccId', sql.Int, id)
//                 .query('DELETE FROM Account_table WHERE AccId = @AccId');
//             res.send('Account deleted');
//         } catch (err) {
//             res.status(500).send(err.message);
//         }
//     });

//     // CRUD operations for Address_table

//     // GET all addresses
//     app.get('/addresses', async (req, res) => {
//         try {
//             const result = await pool.request().query('SELECT * FROM Address_table');
//             res.json(result.recordset);
//         } catch (err) {
//             res.status(500).send(err.message);
//         }
//     });

//     // POST a new address
//     app.post('/addresses', async (req, res) => {
//         const { AccountID, AddressTypeID, Address } = req.body;
//         console.log('Received request to add address:', req.body); // Log request body for debugging
//         try {
//             const result = await pool.request()
//                 .input('AccountID', sql.Int, AccountID)
//                 .input('AddressTypeID', sql.Int, AddressTypeID)
//                 .input('Address', sql.VarChar(255), Address)
//                 .query('INSERT INTO Address_table (AccountID, AddressTypeID, Address) VALUES (@AccountID, @AddressTypeID, @Address); SELECT SCOPE_IDENTITY() AS AddressID;');
//             const newAddressId = result.recordset[0].AddressID;
//             const newAddress = { AddressID: newAddressId, AccountID, AddressTypeID, Address };
//             console.log('Address added successfully'); // Log success message
//             res.status(201).send(newAddress);
//         } catch (err) {
//             console.error('Error adding address:', err); // Log detailed error message
//             res.status(500).send(err.message);
//         }
//     });

//     // PUT (update) an address by ID
//     app.put('/addresses/:id', async (req, res) => {
//         const { id } = req.params;
//         const { AccountID, AddressTypeID, Address } = req.body;
//         try {
//             await pool.request()
//                 .input('AddressID', sql.Int, id)
//                 .input('AccountID', sql.Int, AccountID)
//                 .input('AddressTypeID', sql.Int, AddressTypeID)
//                 .input('Address', sql.VarChar(255), Address)
//                 .query('UPDATE Address_table SET AccountID = @AccountID, AddressTypeID = @AddressTypeID, Address = @Address WHERE AddressID = @AddressID');
//             res.send('Address updated');
//         } catch (err) {
//             res.status(500).send(err.message);
//         }
//     });

//     // DELETE an address by ID
//     app.delete('/addresses/:id', async (req, res) => {
//         const { id } = req.params;
//         try {
//             await pool.request()
//                 .input('AddressID', sql.Int, id)
//                 .query('DELETE FROM Address_table WHERE AddressID = @AddressID');
//             res.send('Address deleted');
//         } catch (err) {
//             res.status(500).send(err.message);
//         }
//     });

//     app.listen(port, () => {
//         console.log(`Server running on port ${port}`);
//     });
// }).catch(err => {
//     console.error('Database connection failed:', err);
// });
