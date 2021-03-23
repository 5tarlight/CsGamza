const fs = require('fs')
const users = []

fs.readdir('./data', (err, files) => {
  files.forEach(f => {
    if (fs.lstatSync(`./data/${f}`).isDirectory())
      users.push({
        name: f,
        valid: false,
      })
  })
})

users.forEach(({ u, v }) => {
  fs.readdir(`./data/${u}`, (err, files) => {
    if (
      files.includes('inventory.idata') &&
      files.includes('profile.pdata')
    ) {
      const index = users.findIndex(us => us.name == u)
      users[index].valid = true
    }
  })
})

console.log(`${users.length} users`)
users.forEach(({ u, v }) => {
  if (v)
    console.log(`${u} - valid`)
  else
    console.log(`${u} - invalid`)
})
