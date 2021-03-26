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

  users.forEach((u) => {
    fs.readdir(`./data/${u.name}`, (err, files) => {
      if (
        files.includes('inventory.idata') &&
        files.includes('profile.pdata') &&
        files.includes('pet.pet')
      ) {
        const index = users.findIndex(us => us.name == u.name)
        users[index].valid = true
      }

      console.log(`${users.length} users`)
      users.forEach(({ name, valid }) => {
        if (valid)
          console.log(`${name} - valid`)
        else
          console.log(`${valid} - invalid`)
      })
    })
  })
})
